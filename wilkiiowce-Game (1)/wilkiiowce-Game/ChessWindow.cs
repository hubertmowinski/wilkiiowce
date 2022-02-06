using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Chess
{
    public partial class Chess : Form {
        public Chess() { InitializeComponent(); }
        public class Opponent
        {
            public string Name;
            public Socket Handler;

            public Opponent(string Name, Socket Handler)
            {
                this.Name = Name;
                this.Handler = Handler;
            }
        }
        Board board;
        //Do komunikacji
        private int Port;
        public static bool Serwer_on;
        public volatile bool Stan = true;
        public Opponent Klient;
        public Thread sluchajacy;
        static Movement piecemoves;
        Tile piecetile, movetile;
        //
        private void Chess_Load(object sender, EventArgs e) {
            board = new Board(this);
            Board.CurrentPlayer = ChessColor.NONE;
            TimeButton.MouseDown += click;
            Undo.MouseDown += click;
            Undo.MouseUp += ReleasedButton;
            RestartButton.MouseUp += click;
            StartButton.MouseUp += click;
            StopButton.MouseUp += click;
            SendMoveButton.MouseUp += click;
        }
        public ChessColor previousPlayer = ChessColor.WHITE;
        void click(object sender, EventArgs e) {
            if (sender.GetType() == typeof(Button))
                switch (((Button)sender).Name) {
                    case "TimeButton": // licznik czasu, do determinowania ile, kto spedził czasu nad mysleniem niestety funkcja przestała działać gdy dodaliśmy możliwosc zaczynania jako czarny
                        if (Board.CurrentPlayer == ChessColor.NONE && previousPlayer == ChessColor.NONE) { TimeButton.Enabled = false; return; }
                        TimeButton.Text = TimeButton.Text == "Start" ? "Stop" : "Start"; //wznowienie
                        if (Board.CurrentPlayer == ChessColor.NONE) StartTimer();
                        else StopTimer();
                        break;
                    case "RestartButton": // Restart, ustawia piony w domyślnym polozeniu resetuje game state
                        int moves = Board.previousMoves.Count;
                        for (int i = 0; i < moves; i++)
                            ShowPreviousMove();
                        Board.previousMoves = new List<PreviousBoardState>();
                        Black = new PlayerTime(0,0);
                        White = new PlayerTime(0, 0);
                        if (timer != null) StopTimer();
                        TimeButton.Text = "Start";
                        Board.CurrentPlayer = ChessColor.WHITE;
                        ToSend("restart/nowy");
                        Board.Window.GameState.Text = "NORMAL";
                        Board.Window.GameState.ForeColor = System.Drawing.Color.OliveDrab;
                        break;
                    case "StartButton":
                        StartButton.Enabled = false;
                        TextPort.Enabled = false;
                        StopButton.Enabled = true;
                        RestartButton.Enabled = true;
                        SendMoveButton.Enabled = true;
                        start();
                        break;
                    case "SendMoveButton": // Przycisk powiązany z komunikacją, służy do przekazania wykonanego ruchu do drugiego uzytkownika
                        SendPreviousMove();
                        break;
                }
            if (sender.GetType() == typeof(PictureBox))
                switch (((PictureBox)sender).Name) {
                    case "Undo":
                        Undo.Image = Properties.Resources.undoArrrowClicked;
                        ShowPreviousMove();
                        break;
                }
        }
        void ShowPreviousMove(){ // Wykorzystane do zapamiętania poprzednich ruchów
            if (Board.previousMoves.Count == 0) return;
            int dir = Board.CurrentPlayer == ChessColor.BLACK ? 1 : -1;
            ChessColor previousPlayer =  ChessColor.WHITE;
            Board.tiles[previousMove.move.y, previousMove.move.x].PieceAssign(previousMove.removedPiece);
            Board.tiles[previousMove.previousLocation.y, previousMove.previousLocation.x].PieceAssign(previousMove.piece);
            Board.previousMoves.RemoveAt(Board.previousMoves.Count - 1);
            Board.CurrentPlayer = previousPlayer;
            ChessPiece.UpdateAllAttacks();
            Movement movement = new Movement();
        }
        void SendPreviousMove()
        {
            PreviousBoardState lastMove = Board.previousMoves.Last();
            if (Board.previousMoves.Count == 0) return;
            string wiadomosc_ruch = "nowyruch/" + lastMove.previousLocation.y + "/"
                + lastMove.previousLocation.x + "/" + lastMove.move.y + "/" + lastMove.move.x;
            ToSend(wiadomosc_ruch);
        }
        void ReleasedButton(object sender, MouseEventArgs e)
        { // służy do możliwości cofania ruchu dopóki się go nie wysłało
            Undo.Image = Properties.Resources.undoArrrow;}
        PreviousBoardState previousMove { get { return Board.previousMoves.Last(); } }
        PlayerTime Black = new PlayerTime(0, 0);
        PlayerTime White = new PlayerTime(0, 0);
        public System.Windows.Forms.Timer timer;
        private void StartTimer() { // zliczanie czasu dla kazdego gracza, tak jak wspominałem ze względu na mozliwość rozpoczecia gry przez wilki czas jest błędnie zliczany
            Board.CurrentPlayer = previousPlayer;
            void ShowLabel(Label label, ref PlayerTime time){
                time.seconds += 1;
                label.Text = time.TimeFormat();
                if (time.seconds >= 59) { time.minutes += 1; }
            }
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (object sender, EventArgs e) => {
                string player = Board.CurrentPlayer.ToString();
                if (player == "BLACK")
                    ShowLabel(BlackTimer, ref Black);
                else ShowLabel(WhiteTimer, ref White);
            };
            timer.Start();
        }
        public void StopTimer() {
            timer.Stop();
            previousPlayer = Board.CurrentPlayer;
            Board.CurrentPlayer = ChessColor.NONE;
            BlackTimer.Text = Black.TimeFormat();
            //WhiteTimer.Text = White.TimeFormat();
        }
        struct PlayerTime{
            public int seconds;
            public int minutes;
            public PlayerTime(int seconds, int minutes) { this.seconds = seconds; this.minutes = minutes; }
            public string TimeFormat(){
                string Format(string time) {
                    if (time.Length == 1) time = "0" + time;
                    return time;
                }
                if (seconds >= 60) seconds = 0;
                return Format(minutes.ToString())+":"+ Format(seconds.ToString());
            }
        }
        public void start()
        {
            Port = Convert.ToInt16(TextPort.Text);
            Serwer_on = true;
            sluchajacy = new Thread(new ThreadStart(Listen));
            sluchajacy.Start();
            System.Console.WriteLine("Server is working...");
            System.Console.WriteLine(Port);
        }
        public void Listen()
        {
            TcpListener Listener = new TcpListener(Port);
            Listener.Start();
            while (Serwer_on)
            {
                Socket Handler = Listener.AcceptSocket();
                if (Handler.Connected)
                {
                    System.Console.WriteLine("Server is working...");
                    string Nazwa;

                    NetworkStream Strumien = new NetworkStream(Handler);
                    int ReadLenghtMsg = 0;
                    int Max = 1024;
                    Byte[] BityOdebrane = new Byte[Max];

                    ReadLenghtMsg = Strumien.Read(BityOdebrane, 0, Max);
                    Nazwa = Encoding.ASCII.GetString(BityOdebrane, 0, ReadLenghtMsg);

                    System.Console.WriteLine("Nazwa klienta: " + Nazwa + Environment.NewLine);
                    Klient = new Opponent(Nazwa, Handler);
                    Polaczenie();
                }
            }
            Listener.Stop();
            System.Console.WriteLine("Wylaczono serwer" + Environment.NewLine);
        }

        public void Polaczenie()
        {
            string text;
            Stan = true;
            Opponent a = Klient;
            Socket handler = (Socket)a.Handler;
            NetworkStream Strumien = new NetworkStream(handler);
            string Komunikat = "Witamy, na serwerze" + Environment.NewLine;
            Strumien.Write(Encoding.ASCII.GetBytes(Komunikat), 0, Komunikat.Length);
            Strumien.Flush();
            Strumien.Write(Encoding.ASCII.GetBytes(a.Name + " "), 0, a.Name.Length);
            Strumien.Flush();
            int ReadLenghtMsg = 0;
            int Max = 1024;
            Byte[] BityOdebrane = new Byte[Max];
            while (Stan == true)
            {
                ReadLenghtMsg = Strumien.Read(BityOdebrane, 0, Max);
                text = Encoding.ASCII.GetString(BityOdebrane, 0, ReadLenghtMsg);
                string[] subs = text.Split(new char[] { '/' });
                if (subs[0] == "nowyruch")
                {
                    int y_piece = int.Parse(subs[1]);
                    int x_piece = int.Parse(subs[2]);
                    int y_move = int.Parse(subs[3]);
                    int x_move = int.Parse(subs[4]);
                    NewRemoteMove(y_piece, x_piece, y_move, x_move);
                }
                else if (subs[0] == "restart")
                {
                    int moves = Board.previousMoves.Count;
                    for (int i = 0; i < moves; i++)
                        ShowPreviousMove();
                    Board.previousMoves = new List<PreviousBoardState>();
                    Black = new PlayerTime(0, 0);
                    White = new PlayerTime(0, 0);
                    if (timer != null) StopTimer();
                    //TimeButton.Text = "Start";
                    Board.CurrentPlayer = ChessColor.WHITE;
                }
                System.Console.WriteLine(a.Name + ":" + text + Environment.NewLine);
                System.Console.WriteLine(a.Name + ":" + text);
            }
            handler = null;
            Strumien.Close();
        }
        
        void NewRemoteMove(int y_piece, int x_piece, int y_move, int x_move)
        {
            Board.CurrentPlayer = ChessColor.BLACK;
            piecetile = Board.GetTileYX(y_piece, x_piece);
            movetile = Board.GetTileYX(y_move, x_move);
            piecemoves = new Movement(piecetile);
            piecemoves.MovesInterface(true);
            piecemoves.isAvailableMove(movetile, true);

        }

        public void ToSend(string Tekst)
        {
            System.Console.WriteLine(Tekst);
            Socket handler = (Socket)Klient.Handler;
            NetworkStream Strumien = new NetworkStream(handler);
            Strumien.Write(Encoding.ASCII.GetBytes(Tekst), 0, Encoding.ASCII.GetBytes(Tekst).Length);
            Strumien.Flush();
        }

        private void TimeButton_Click(object sender, EventArgs e)
        {

        }

        private void RestartButton_Click(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {

        }
    }
}