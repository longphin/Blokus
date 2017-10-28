using System;
using Blokus;
using System.Collections.Generic;

namespace Main
{
    class Program
    {
        Board board;

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
                Console.WriteLine(p.name + " Please check your available choices");
                string input = Console.ReadLine();

                while(input.ToLower()!="end")
                {
                    switch (input.ToLower())
                    {
                        case "open":
                            p.PrintAvailableCells();
                            break;
                        case "pieces":
                            p.PrintAvailablePieces();
                            break;
                        case "rotate":
                            Console.WriteLine("Rotate what piece?");
                            bool rotated = p.RotatePiece(Console.ReadLine());
                            if (rotated == true) Console.WriteLine("Success"); else Console.WriteLine("Unknown piece");
                            break;
                        default:
                            Console.WriteLine("Please enter a valid option.");
                            break;
                    }
                    input = Console.ReadLine();
                }
            }
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