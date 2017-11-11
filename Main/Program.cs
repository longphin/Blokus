using System;
using Blokus;
using System.Collections.Generic;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            // error check if number of args is matched
            if (args.Length < 1)
            {
                Console.WriteLine("Please enter the board size. For example, \"Blokus 14\" creates a 14x14 board");
                return;
            }

            // error check if inputs are integers
            int n;
            if (!Int32.TryParse(args[0], out n))
            {
                Console.WriteLine("Board size must be integers. For example, \"Blokus 14\" creates a 14x14 board");
                return;
            }

            // Initialize Board
            Console.WriteLine("----- Initiating Blokus -----");

            Console.WriteLine(n.ToString() + " x " + n.ToString());
            var b = new Board(n, n);
            b.PrintBoardText();

            // Create Players
            List<Player> players = new List<Player>();

            var p1 = new Player("Player 1");
            p1.SetCellAsAvailable(new int[] { 0, 0 });

            var p4 = new Player("Player 4");
            p4.SetCellAsAvailable(new int[] { n - 1, n - 1 });

            players.Add(p1);
            players.Add(p4);

            // Add pieces to player
            AddPiecesForPlayer(p1, 1);
            AddPiecesForPlayer(p4, 2);

            // Ask for input
            foreach (var p in players)
            {
                string input = AskPlayerInput(p);

                while (input!="end")
                {
                    bool validCommand = false;
                    string[] splitinput = input.Split(' ');

                    if(input == "board")
                    {
                        validCommand = true;
                        b.PrintBoardText();
                    }

                    if (input == "open")
                    {
                        validCommand = true;
                        p.PrintAvailableCells();
                    }

                    if (input == "pieces")
                    {
                        validCommand = true;
                        p.PrintAvailablePieces();
                    }

                    if (splitinput[0] == "rotate")
                    {
                        validCommand = true;
                        if (splitinput.Length < 2)
                        {
                            Console.WriteLine("Rotate requires the piece name, and an optional degree to rotate");
                            input = AskPlayerInput(p);
                            continue;
                        }

                        double degree = 90.0;
                        if (splitinput.Length == 3)
                        {
                            if(Double.TryParse(splitinput[2], out degree))
                            {
                                if (degree % 90.0 != 0.0)
                                {
                                    Console.WriteLine("Rotation must be a multiple of 90 degrees.");
                                    input = AskPlayerInput(p);
                                    continue;
                                }
                            }
                        }
                        string actualpiecename; // RotatePiece() will give the actual piece name, so if player types in "r", the piece name will match, but say "R".
                        bool rotated = p.RotatePiece(splitinput[1], degree, out actualpiecename);
                        if (rotated == true) Console.WriteLine("Rotated " + actualpiecename); else Console.WriteLine("Unknown piece");
                    }

                    if (splitinput[0] == "flip")
                    {
                        validCommand = true;
                        if (splitinput.Length < 2)
                        {
                            Console.WriteLine("Flip requires the piece name");
                            input = AskPlayerInput(p);
                            continue;
                        }

                        string actualpiecename; // RotatePiece() will give the actual piece name, so if player types in "r", the piece name will match, but say "R".
                        bool flipped = p.FlipPiece(splitinput[1], out actualpiecename);
                        if (flipped == true) Console.WriteLine("Flipped " + actualpiecename); else Console.WriteLine("Unknown piece");
                    }

                    if (input == "validate")
                    {
                        validCommand = true;
                        foreach(KeyValuePair<int[], int> entry in p.availableCells)
                        {
                            int[] avail = entry.Key;
                            foreach (var piece in p.pieces)
                            {
                                Console.WriteLine("Checking " + piece.name);

                                bool canFit = b.CanPieceFitCell(piece, p.id, new int[] { avail[0], avail[1] });
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                canFit = b.CanPieceFitCell(piece, p.id, new int[] { avail[0], avail[1] });
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                canFit = b.CanPieceFitCell(piece, p.id, new int[] { avail[0], avail[1] });
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                canFit = b.CanPieceFitCell(piece, p.id, new int[] { avail[0], avail[1] });
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                piece.FlipPiece();
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                canFit = b.CanPieceFitCell(piece, p.id, new int[] { avail[0], avail[1] });
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                canFit = b.CanPieceFitCell(piece, p.id, new int[] { avail[0], avail[1] });
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                canFit = b.CanPieceFitCell(piece, p.id, new int[] { avail[0], avail[1] });
                                if (canFit == true) piece.PrintPiece();

                                piece.RotatePiece(90.0);
                                piece.FlipPiece();
                            }
                        }
                    }

                    if (validCommand == false)
                    {
                        Console.WriteLine("Please enter a valid command.");
                        PrintValidCommands();
                    }
                    input = AskPlayerInput(p);
                }
            }
        }

        private static string AskPlayerInput(Player p)
        {
            Console.Write(" > [" + p.name + " " + p.id.ToString() + "] ");
            return(Console.ReadLine().ToLower());
        }

        private static void PrintValidCommands()
        {
            Console.WriteLine("=== List of commands ===");
            Console.WriteLine(" board: Prints board state.");
            Console.WriteLine(" open: Shows a list of available open spots on the board.");
            Console.WriteLine(" pieces: Shows a list remaining pieces that you have.");
            Console.WriteLine(" rotate [piece name] [degrees]: Rotates a piece 90, 180, or 270 degrees. Defaults to 90.");
            Console.WriteLine(" flip [piece name]: Flips a piece along the y axis.");
            Console.WriteLine(" validate: Shows which pieces can be placed.");
            Console.WriteLine(" end: ends player's turn.");
        }

        private static void AddPiecesForPlayer(Player p, int pint)
        {
            p.AddPiece(new Piece("I1",
                new List<int[]> { new int[] { 0, 0 } }));
            p.AddPiece(new Piece("I2",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 } }));
            p.AddPiece(new Piece("I3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece("I4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 } }));
            p.AddPiece(new Piece("I5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 }, new int[] { 0, 4 } }));
            p.AddPiece(new Piece("L3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece("L4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece("L5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 0, 3 } }));
            p.AddPiece(new Piece("V5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 2, 0 } }));
            p.AddPiece(new Piece("Z4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("Z5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 1, 0 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("O4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("T5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } }));
            p.AddPiece(new Piece("T4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece("N",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, -1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { 2, 0 } }));
            p.AddPiece(new Piece("P",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 1 }, new int[] { 1, 0 } }));
            p.AddPiece(new Piece("W",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("U",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, -1 }, new int[] { 1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("F",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("X",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 } }));
            p.AddPiece(new Piece("R",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 2, 0 } }));

            /*
            p.AddPiece(new Piece("I1",
                new List<int[]> { new int[] { 0, 0 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 1 }, new int[] { 1, -1 }, new int[] { 1, 1 } }));
            p.AddPiece(new Piece("I2",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 2 }, new int[] { 1, -1 }, new int[] { 1, 2 } }));
            p.AddPiece(new Piece("I3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 3 }, new int[] { 1, -1 }, new int[] { 1, 3 } }));
            p.AddPiece(new Piece("I4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 4 }, new int[] { 1, -1 }, new int[] { 1, 4 } }));
            p.AddPiece(new Piece("I5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 }, new int[] { 0, 4 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 5 }, new int[] { 1, -1 }, new int[] { 1, 5 } }));
            p.AddPiece(new Piece("L3",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 2 }, new int[] { 1, 2 }, new int[] { 2, 1 }, new int[] { 2, -1 } }));
            p.AddPiece(new Piece("L4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 3 }, new int[] { 1, 3 }, new int[] { 2, 1 }, new int[] { 2, -1 } }));
            p.AddPiece(new Piece("L5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 0, 3 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 4 }, new int[] { 1, 4 }, new int[] { 2, 1 }, new int[] { 2, -1 } }));
            p.AddPiece(new Piece("V5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, 2 }, new int[] { 2, 0 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 3 }, new int[] { 1, 3 }, new int[] { 3, 1 }, new int[] { 3, -1 } }));
            p.AddPiece(new Piece("Z4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 } },
                new List<int[]> { new int[] { -2, -1 }, new int[] { -2, 1 }, new int[] { -1, 2 }, new int[] { 2, 2 }, new int[] { 2, 0 }, new int[] { 1, -1 } }));
            p.AddPiece(new Piece("Z5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 1, 0 }, new int[] { 1, 1 } },
                new List<int[]> { new int[] { -2, -2 }, new int[] { -2, 1 }, new int[] { 0, 2 }, new int[] { 2, 2 }, new int[] { 2, -1 }, new int[] { 0, -2 } }));
            p.AddPiece(new Piece("O4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 1 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 2 }, new int[] { 2, 2 }, new int[] { 2, -1 } }));
            p.AddPiece(new Piece("T5",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 } },
                new List<int[]> { new int[] { -2, -1 }, new int[] { -2, 1 }, new int[] { -1, 4 }, new int[] { 1, 4 }, new int[] { 2, 1 }, new int[] { 2, -1 } }));
            p.AddPiece(new Piece("T4",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } },
                new List<int[]> { new int[] { -2, -1 }, new int[] { -2, 1 }, new int[] { -1, 2 }, new int[] { 1, 2 }, new int[] { 2, 1 }, new int[] { 2, -1 } }));
            p.AddPiece(new Piece("N",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, -1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { 2, 0 } },
                new List<int[]> { new int[] { -2, -2 }, new int[] { -2, 0 }, new int[] { -1, 1 }, new int[] { 3, 1 }, new int[] { 3, -1 } }));
            p.AddPiece(new Piece("P",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 1 }, new int[] { 1, 0 } },
                new List<int[]> { new int[] { -1, -1 }, new int[] { -1, 3 }, new int[] { 1, 3 }, new int[] { 2, 2 }, new int[] { 2, -1 } }));
            p.AddPiece(new Piece("W",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { -1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } },
                new List<int[]> { new int[] { -1, 2 }, new int[] { -2, 1 }, new int[] { -1, 2 }, new int[] { 2, 2 }, new int[] { 2, 0 }, new int[] { 1, -1 }, new int[] { 0, -2 } }));
            p.AddPiece(new Piece("U",
                new List<int[]> { new int[] { 0, 0 }, new int[] { 0, -1 }, new int[] { 1, -1 }, new int[] { 0, 1 }, new int[] { 1, 1 } },
                new List<int[]> { new int[] { -1, 2 }, new int[] { -2, 1 }, new int[] { -1, 2 }, new int[] { 2, 2 }, new int[] { 2, 0 }, new int[] { 1, -1 }, new int[] { 0, -2 } }));
            p.AddPiece(new Piece("F",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 1 } },
                new List<int[]> { new int[] { -2, 1 }, new int[] { -1, 2 }, new int[] { 2, 2 }, new int[] { 2, 0 }, new int[] { 1, -2 }, new int[] { -1, -2 }, new int[] { -2, -1 } }));
            p.AddPiece(new Piece("X",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 0, -1 } },
                new List<int[]> { new int[] { -2, 1 }, new int[] { -1, 2 }, new int[] { 1, 2 }, new int[] { 2, 1 }, new int[] { 2, -1 }, new int[] { 2, -1 }, new int[] { 1, -2 }, new int[] { 1, -2 }, new int[] { -1, -2 }, new int[] { -2, -1 } }));
            p.AddPiece(new Piece("R",
                new List<int[]> { new int[] { 0, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 2, 0 } },
                new List<int[]> { new int[] { -2, 1 }, new int[] { -1, 2 }, new int[] { 1, 2 }, new int[] { 3, 1 }, new int[] { 3, -1 }, new int[] { -2, -1 } }));
            */
        }
    }
}