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

        public Board()
        {
            board = new int[14, 14];
        }
        public Board(int nrows, int ncolumns)
        {
            board = new int[nrows, ncolumns];
        }

        // check if cell at (n, m) is empty
        public bool IsCellEmpty(int n, int m)
        {
            return (board[n, m] == 0 ? true : false);
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

                for (int j=0; j<board.GetLength(1); j++)
                {
                    Console.Write(board[i, j].ToString() + "\t");
                }
                Console.Write("\n");
            }
        }
    }
}
