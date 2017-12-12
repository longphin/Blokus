using System;
using System.Collections.Generic;

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
                    if (piece.count == 0) continue;

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
                            Move m = new Move(p.id, piece.Clone(1), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[2] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(2), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[3] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(3), coord, validCenters);
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
                            Move m = new Move(p.id, piece.Clone(4), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[5] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(5), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[6] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(6), coord, validCenters);
                            possibleMoves.Add(m);
                        }
                    }

                    piece.RotatePiece(90.0);
                    if (validOrientations[7] == true)
                    {
                        List<int> validCenters = CanPieceFitCell(piece, p.id, new int[] { coord[0], coord[1] });
                        if (validCenters.Count > 0)
                        {
                            Move m = new Move(p.id, piece.Clone(7), coord, validCenters);
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
                int[] center = new int[] { cellLoc[0] - (p.points[i])[0], cellLoc[1] - (p.points[i])[1] };

                for (int j=0; j<p.points.Count; j++) // check all of the pieces when i is the center point
                {
                    int px = center[0] + (p.points[j])[0];
                    int py = center[1] + (p.points[j])[1];

                    // if the piece fails any of these conditions, then it can not fit in the cell
                    if (IsCellValidForPlayer(px, py, playerID)==false
                        )
                    {
                        doesEntirePieceFit = false;
                        break;
                    }
                }

                if (doesEntirePieceFit == true)
                    validCenters.Add(i);
            }
            return (validCenters);
        }

        /// <summary>
        /// Makes the chosen move for the player. The count of the piece used will be decremented by 1. If the piece's corners are valid future positions,
        /// then they will be added to the player's availableCells.
        /// A call the UpdatePlayerAvailableCells() will need to be called afterwards.
        /// </summary>
        /// <param name="player">Player to make the move</param>
        /// <param name="m">The move.</param>
        /// <param name="SelectedCenter">Each move has a list of valid centers that the piece can use. SelectedCenter is the chosen center of the move's list.
        /// That is, move.validCenters[SelectedCenter] will be the INDEX of the move.piece.points[] that is used as the center.</param>
        public void MakeMove(List<Player> players, Player player, Move m, int SelectedCenter)
        {
            var p = m.piece;
            int[] cellLoc = m.PlacePieceAt;
            int[] center = new int[] { cellLoc[0] - (p.points[m.validCenters[SelectedCenter]])[0], cellLoc[1] - (p.points[m.validCenters[SelectedCenter]])[1] };

            for (int j = 0; j < p.points.Count; j++) // check all of the pieces when i is the center point
            {
                int px = center[0] + (p.points[j])[0];
                int py = center[1] + (p.points[j])[1];
                board[px, py] = player.id;
            }
            
            foreach(var pl in players)
            {
                UpdatePlayerAvailableCells(pl);
            }
            
            // Update this player's corners by looking at p.corners. If the corner (adjusted with center) is not occupied and is not adjacent to the player's piece,
            //        then mark it as available.
            for(int j = 0; j<p.corners.Count; j++)
            {
                int px = center[0] + (p.corners[j])[0];
                int py = center[1] + (p.corners[j])[1];

                if (IsCellValidForPlayer(px, py, m.playerID))
                {
                    // corner is valid, so add it to player's available cells
                    player.SetCellAsAvailable(new int[] { px, py });
                }
            }
            
            // reduce the number of pieces of this type available to the player now
            foreach (var v in player.pieces)
            {
                if(v.id==p.id)
                {
                    v.count -= 1;
                }
            }
        }

        private void UpdatePlayerAvailableCells(Player p)
        {
            // temporary list to keep track of which keys to remove
            List<int[]> keysToDelete = new List<int[]>();

            foreach(var k in p.availableCells.Keys)
            {
                if (IsCellValidForPlayer(k[0], k[1], p.id) == false)
                    keysToDelete.Add(k);
            }

            foreach(var k in keysToDelete)
            {
                p.SetCellAsUnavailable(k);
            }
        }

        private bool IsCellValidForPlayer(int px, int py, int playerID)
        {
            if(px < 0 || py < 0 || px > board.GetLength(0) - 1 || py > board.GetLength(1) - 1 // piece is outside of bounds
                    || board[px, py] != 0 // there are pieces in the way
                    || (px - 1 >= 0 && board[px - 1, py] == playerID) // there are adjacent player pieces
                    || (px + 1 < board.GetLength(0) && board[px + 1, py] == playerID)
                    || (py - 1 >= 0 && board[px, py - 1] == playerID)
                    || (py + 1 < board.GetLength(1) && board[px, py + 1] == playerID)
                    )
            {
                return (false);
            }
            else
            {
                return (true);
            }
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
                    Console.Write((board[i, j]==0 ? "-" : board[i, j].ToString()) + "\t");
                }
                Console.Write("\n");
            }
        }
    }
}
