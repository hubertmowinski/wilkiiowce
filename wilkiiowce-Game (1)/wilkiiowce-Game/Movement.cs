using System.Collections.Generic;

namespace Chess
{
    public class Movement {
        Tile pieceTile;
        public PieceMove?[] availableMoves;
        public Movement() { previousMove = new PieceMove(1, 1); }
        public Movement(Tile pieceTile):this(){
            this.pieceTile = pieceTile;
            availableMoves = Moves();
        }
        public bool isAvailableMove(Tile clickedTile, bool remoteMove){
            MovesInterface(false);
            for (int move = 0; move < availableMoves.Length; move++)
            {
                if (availableMoves[move] == null) continue;
                if (availableMoves[move]?.x == clickedTile.location.x && availableMoves[move]?.y == clickedTile.location.y)
                {
                    if (!CanPerformPieceAction(move, clickedTile)) continue;
                    Board.previousMoves.Add(new PreviousBoardState(pieceTile.piece, pieceTile.location, (PieceMove)availableMoves[move], clickedTile.piece, move));
                    pieceTile.piece.firstMove = false;
                    clickedTile.PieceAssign(pieceTile.piece);
                    pieceTile.PieceAssign(new ChessPiece(PieceKind.EMPTY));
                    if (!PieceSelection.waiting)
                    {
                        if (remoteMove)
                        {
                            Board.CurrentPlayer = ChessColor.WHITE;
                        }
                        else
                        {
                            Board.CurrentPlayer = ChessColor.NONE;
                        }
                        ChessPiece.UpdateAllAttacks();
                        if (KingAttacked())
                        {
                            Board.Window.GameState.Text = "CHECK";
                            Board.Window.GameState.ForeColor = System.Drawing.Color.Firebrick;
                            CheckMate();
                        }
                        else
                        {
                            //Board.Window.GameState.Text = "NORMAL";
                            Board.Window.GameState.ForeColor = System.Drawing.Color.OliveDrab;
                        }

                    }
                    return true;
                }
            }
            CheckMate(); // Jeśli nie ma możliwego ruchu szach-mat
            return false;
        }
        public void MovesInterface(bool show)
        {
            for (int i = 0; i < availableMoves.Length; i++)
            {
                if (availableMoves[i] == null) continue;
                if (pieceTile.piece.piecekind == PieceKind.Pawn && (i == 2 && isNotOccupied(pieceTile.location.y + dir, pieceTile.location.x + 1)
                    || i == 3 && isNotOccupied(pieceTile.location.y + dir, pieceTile.location.x - 1))) continue;
                Board.tiles[(int)availableMoves[i]?.y, (int)availableMoves[i]?.x].PossibleMove(show);
            }
        }
        public bool KingAttacked() => Board.GetTile(PieceKind.Bishop, ChessColor.WHITE).tileAttack != ChessColor.NONE;
           public bool CheckMate(){
               List<Tile> pieceTiles = Board.GetAllPieceTiles(Board.CurrentPlayer);
               for (int j = 0; j < pieceTiles.Count; j++){
                   pieceTile = pieceTiles[j];
                   availableMoves = Moves();
               //    if (IsKingSafe()) 
                    //   return false;
               }
               Board.Window.GameState.Text = "CHECKMATE";
               Board.Window.GameState.ForeColor = System.Drawing.Color.Firebrick;
               Board.Window.StopTimer();
               return true;
           }
        
        bool CanPerformPieceAction(int move, Tile clickedTile)
        {
            switch (pieceTile.piece.piecekind)
            {
                default: break;
            }
            return true;
        } // Nie używana funkcja
        
        PieceMove? previousMove; 
        int move;
        PieceMove?[] Moves() {
            PieceMove?[] moves = new PieceMove?[8];
            move = 0;
            switch (pieceTile.piece.piecekind) {
                case PieceKind.Knight: // ruch rycerza czyli naszych wilków
                    moves[0] = destination(1, -1);
                    moves[1] = destination(1, 1);
                    break;

                case PieceKind.Bishop: // Figura bishop'a czyli naszej owcy
                    moves[0] = destination(1, -1);
                    moves[1] = destination(1, 1);
                    moves[2] = destination(-1, 1);
                    moves[3] = destination(-1, -1);
                    break;
            }
            return moves;
        }
        int dir { get { return pieceTile.piece.color == ChessColor.BLACK ? 1 : -1; } }
        PieceMove? destination(int y, int x) { // Dla figur z konkretnymi ruchami

            int yLocation = pieceTile.location.y + y;
            int xLocation = pieceTile.location.x + x;
            move++;
            if (isAccessAble(yLocation, xLocation)){ // Sprawdza czy jest pole dostępne, czyli czy jest zajetę i czy color jest przeciwnika czy swojego
                if (isOccupied(yLocation, xLocation) && pieceTile.piece.color != EnemyColor(yLocation, xLocation) && pieceTile.piece.color == EnemyColor(yLocation, xLocation)){
                    previousMove = null;
                    Board.GetTile(PieceKind.Bishop, ChessColor.WHITE); // Warunek dla owcy jeśli dojdzie do konca planszy, szach-mat
                    if (Board.tiles[y, x] == Board.tiles[0, x])
                    {
                        CheckMate();
                    }
                    return new PieceMove(yLocation, xLocation);
                }
                if (isOccupied(yLocation, xLocation)) return previousMove = null;
                return previousMove = new PieceMove(yLocation, xLocation);
            }
            else return previousMove = null;
        }
        bool isAccessAble(int y, int x) => Board.isAccessAble(y,x); // Sprawdza czy cos się znajduje na polu i czy jest to sojusznik
        bool isOccupied(int y, int x) => Board.tiles[y, x].isOccupied(); // Sprawdza czy coś się znajduje na wybranym polu
        bool isEnemyPawn(int y, int x){
            if (isAccessAble(y, x)) return isOccupied(y, x) && Board.GetPiece(y, x).TwoTilesMove && isEnemyColor(y, x);
            else return false;
        }
        bool isNotOccupied(int y, int x){ // Jesli jest dostepny to nie jest zajety
            if (isAccessAble(y, x)) return !isOccupied(y, x);
            return false;
        }
        bool isEnemyColor(int y, int x) => pieceTile.piece.color != EnemyColor(y, x) && EnemyColor(y, x) != ChessColor.NONE; // Sprawdza kolor aktywnego gracza i uznaje przeciwny za wrogi
        ChessColor EnemyColor(int y, int x) => Board.tiles[y, x].piece.color;
    }
    public struct PieceMove{
        public int y, x;
        public PieceMove(int y,int x){ this.y = y; this.x = x;  }
    }
}