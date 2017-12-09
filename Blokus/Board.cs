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

        public List<Move> GetPossibleMovesForPlayer(Player p)
        {
            List<Move> res = new List<Move>();

            foreach(var cellLoc in p.availableCells)
            {
                foreach (var piece in p.pieces)
                {
                    List<Move> possibleMoves = new List<Move>();
                    int[] coord = cellLoc.Key;
                    bool[] validOrientations = Piece.UniqueOrientations[piece.id];

                    if (validOrientations[0] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[1] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[2] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[3] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    piece.FlipPiece();
                    if (validOrientations[4] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[5] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[6] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[7] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(0), coord, validCenters);
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

        public List<int> CanPieceFitCell(Piece p, int playerID, int[] cellLoc)
        {
            List<int> validCenters = new List<int>();
            for(int i=0; i<p.points.Count; i++) // each point will get a chance to be placed at the cellLoc
            {
                bool doesEntirePieceFit = true;

                for(int j=0; j<p.points.Count; j++) // check all of the pieces when i is the center point
                {
                    int px = cellLoc[0] - (p.points[i])[0] + (p.points[j])[0];
                    int py = cellLoc[1] - (p.points[i])[1] + (p.points[j])[1];

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
                    //return (true);
                    validCenters.Add(i);
            }
            return (validCenters);
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
