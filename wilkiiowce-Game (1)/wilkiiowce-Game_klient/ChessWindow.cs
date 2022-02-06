using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Chess
{
    public partial class Chess : Form {
        //I could write a better code, but I wanted to sprint a little bit , so I can finally learn further OpenGL. So please don't judge me xD
        //So... the code for this project was written in one week.
        public Chess() { InitializeComponent(); }
        
        Board board;
        //Do komunikacji
        private string Name;
        private IPAddress IP;
        private int Port;
        private TcpClient tcpClient;
        private NetworkStream Strumien;
        private bool Stan;
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
        public ChessColor previousPlayer = ChessColor.BLACK;
        void click(object sender, EventArgs e) {
            if (sender.GetType() == typeof(Button))
                switch (((Button)sender).Name) {
                    case "TimeButton":
                        if (Board.CurrentPlayer == ChessColor.NONE && previousPlayer == ChessColor.NONE) { TimeButton.Enabled = false; return; }
                        TimeButton.Text = TimeButton.Text == "Start" ? "Stop" : "Start"; //Resume...
                        if (Board.CurrentPlayer == ChessColor.NONE) StartTimer();
                        else StopTimer();
                        break;
                    case "RestartButton":
                        int moves = Board.previousMoves.Count;
                        for (int i = 0; i < moves; i++)
                            ShowPreviousMove();
                        Board.previousMoves = new List<PreviousBoardState>();
                        Black = new PlayerTime(0,0);
                        White = new PlayerTime(0, 0);
                        if (timer != null) StopTimer();
                        TimeButton.Text = "Start";
                        Board.CurrentPlayer = ChessColor.BLACK;
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
                    case "SendMoveButton":
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
        void ShowPreviousMove(){
            if (Board.previousMoves.Count == 0) return;
            int dir = Board.CurrentPlayer == ChessColor.BLACK ? 1 : -1;
            ChessColor previousPlayer =  ChessColor.BLACK;
            if (previousMove.piece.piecekind == PieceKind.King){
                int ydir = previousPlayer == ChessColor.WHITE ? 7 : 0;
                if (previousMove.moveIndex == 8){
                    Board.tiles[ydir, 5].PieceAssign(new ChessPiece(PieceKind.EMPTY));
                    Board.tiles[ydir, 7].PieceAssign(new ChessPiece(PieceKind.Rook, previousPlayer));
                }
                if (previousMove.moveIndex == 9){
                    Board.tiles[ydir, 3].PieceAssign(new ChessPiece(PieceKind.EMPTY));
                    Board.tiles[ydir, 0].PieceAssign(new ChessPiece(PieceKind.Rook, previousPlayer));
                }
            }
            if (previousMove.piece.piecekind == PieceKind.Pawn){
                if (previousMove.moveIndex == 4)
                    Board.tiles[previousMove.move.y + dir, previousMove.move.x].PieceAssign(new ChessPiece(PieceKind.Pawn, Board.CurrentPlayer, true));
                if (previousMove.moveIndex == 5)
                    Board.tiles[previousMove.move.y + dir, previousMove.move.x].PieceAssign(new ChessPiece(PieceKind.Pawn, Board.CurrentPlayer, true));
            }
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
        void ReleasedButton(object sender, MouseEventArgs e) { 
            Undo.Image = Properties.Resources.undoArrrow;}
        PreviousBoardState previousMove { get { return Board.previousMoves.Last(); } }
        PlayerTime Black = new PlayerTime(0, 0);
        PlayerTime White = new PlayerTime(0, 0);
        public System.Windows.Forms.Timer timer;
        private void StartTimer() {
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
            //BlackTimer.Text = Black.TimeFormat();
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
            Name = TextAddress.Text;
            IP = IPAddress.Parse(TextAddress.Text);
            Port = Convert.ToInt16(TextPort.Text);
            tcpClient = new TcpClient(TextAddress.Text, Port);
            Strumien = tcpClient.GetStream();
            Stan = true;
            System.Console.WriteLine("Klient laczy sie na porcie: z IP :" + Port + IP);
            System.Console.WriteLine(Port);
            Strumien.Write(Encoding.ASCII.GetBytes(Name), 0, Encoding.ASCII.GetBytes(Name).Length);
            Strumien.Flush();

            byte[] BityOdebrane = new byte[1024];
            int k = 0;
            sluchajacy = new Thread(new ThreadStart(Odbieranie_DoWork));
            sluchajacy.Start();
            //Odbieranie.RunWorkerAsync();
        }
        
        private void Odbieranie_DoWork()
        {
            byte[] BityOdebrane = new byte[1024];
            int k = 0;
            while (Stan) //Jeśli stan is true - wykonuj pętle
            {
                Strumien.Flush(); //Czyszczenie bufora
                string text;
                int i = 0;
                int Dlugosc = Strumien.Read(BityOdebrane, 0, BityOdebrane.Length);
                text = Encoding.ASCII.GetString(BityOdebrane, 0, Dlugosc);
                System.Console.WriteLine(text);
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
                    for (int j = 0; j < moves; j++)
                        ShowPreviousMove();
                    Board.previousMoves = new List<PreviousBoardState>();
                    Black = new PlayerTime(0, 0);
                    White = new PlayerTime(0, 0);
                    if (timer != null) StopTimer();
                    //TimeButton.Text = "Start";
                    Board.CurrentPlayer = ChessColor.BLACK;
                }
            }
        }
        void NewRemoteMove(int y_piece, int x_piece, int y_move, int x_move)
        {
            Board.CurrentPlayer = ChessColor.WHITE;
            piecetile = Board.GetTileYX(y_piece, x_piece);
            movetile = Board.GetTileYX(y_move, x_move);
            piecemoves = new Movement(piecetile);
            piecemoves.MovesInterface(true);
            piecemoves.isAvailableMove(movetile, true);

        }

        public void ToSend(string Tekst)
        {
            System.Console.WriteLine(Tekst);
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