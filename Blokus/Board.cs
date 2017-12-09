using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blokus
{
    public class Board
    {
        private int[,] board; // [0] = num rows, [1] = num columns
        
        public Board(int nrows, int ncolumns)
        {
            board = new int[nrows, ncolumns];
        }

        // check if cell at (n, m) is empty
        public bool IsCellEmpty(int n, int m)
        {
            return (board[n, m] == 0 ? true : false);
        }

        /* // need to check if piece can be placed into a slot
        // checks if a piece can fit the cell. Tests all rotations as well as the flipped + rotations. i.e. 8 tests
        public bool CanPieceFitCell(int n, int m, Piece p)
        {
            bool canFit = false;
            
            return (canFit);
        }
        public bool DoesPieceOverlap(Piece p)
        {
            return (true);
        }
        */
        public List<Move> GetPossibleMovesForPlayer(Player p)
        {
            List<Move> res = new List<Move>();

            foreach(var cellLoc in p.availableCells)
            {
                foreach (var piece in p.pieces)
                {
                    List<Move> possibleMoves = new List<Move>();
                    int[] coord = cellLoc.Key;
                    
                    if (piece.validOrientations[0] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (piece.validOrientations[1] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (piece.validOrientations[2] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (piece.validOrientations[3] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    piece.FlipPiece();
                    if (piece.validOrientations[4] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (piece.validOrientations[5] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (piece.validOrientations[6] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (piece.validOrientations[7] == true)
                    {
                        bool canFit = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (canFit == true)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    piece.FlipPiece();

                    //res.AddRange(possibleMoves.Distinct().ToList());
                    res.AddRange(possibleMoves);
                }
            }

            return (res);
        }

        public bool CanPieceFitCell(Piece p, int playerID, int[] cellLoc)
        {
            foreach (int[] i in p.points) // each point will get a chance to be placed at the cellLoc
            {
                bool doesEntirePieceFit = true;
                
                foreach (int[] j in p.points) // check all of the pieces when i is the center point
                {
                    int px = cellLoc[0] - i[0] + j[0];
                    int py = cellLoc[1] - i[1] + j[1];

                    // if the piece fails any of these conditions, then it can not fit in the cell
                    if (px < 0 || py < 0 || px > board.GetLength(0) - 1 || py > board.GetLength(1) - 1 // piece is outside of bounds
                        || board[px, py] != 0 // there are pieces in the way
                        || (px - 1 >= 0 && board[px - 1, py] == playerID) // there are adjacent player pieces
                        || (px + 1 < board.GetLength(0) && board[px + 1, py] == playerID)
                        || (py - 1 >= 0 && board[px, py - 1] == playerID)
                        || (py + 1 < board.GetLength(1) && board[px, py + 1] == playerID)
                        )
                    {
                        doesEntirePieceFit = false;
                        break;
                    }
                }

                if (doesEntirePieceFit == true)
                    return (true);
            }
            return (false);
        }

        // print the board in text form
        public void PrintBoardText()
        {
            Console.Write("\t");
            for (int j=0; j<board.GetLength(1); j++)
            {
                Console.Write(j.ToString() + "\t");
            }
            Console.Write("\n");

            for (int i=0; i<board.GetLength(0); i++)
            {
                Console.Write(i.ToString() + "\t");

                for (int j=board.GetLength(1)-1; j>=0; j--)
                {
                    Console.Write(board[i, j].ToString() + "\t");
                }
                Console.Write("\n");
            }
        }
    }
}
