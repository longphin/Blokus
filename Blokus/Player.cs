using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blokus
{
    public class Player
    {
        public string name { get; set; }
        public IDictionary<int[], int> availableCells = new Dictionary<int[], int>();
        private List<Piece> pieces = new List<Piece>();

        public Player(string name)
        {
            this.name = name;
        }

        public void AddPiece(Piece p)
        {
            pieces.Add(p);
        }

        public void PrintAvailableCells()
        {
            foreach(KeyValuePair<int[], int> entry in availableCells)
            {
                int[] key = entry.Key;
                string s = "(";
                for(int i=0; i<key.Length; i++)
                {
                    s += key[i].ToString();
                    if (i != key.Length - 1) s += ",";
                }
                s += ")";
                Console.WriteLine(s);
            }
        }

        public void PrintAvailablePieces()
        {
            foreach(var p in pieces)
            {
                Console.WriteLine(" -- " + p.name + "--");
                p.PrintPiece();
            }
        }

        public bool RotatePiece(string pieceName)
        {
            bool rotated = false;
            foreach(var p in pieces)
            {
                if (p.name == pieceName)
                {
                    p.RotatePiece(90.0);
                    rotated = true;
                }
            }
            return (rotated);
        }

        public void SetCellAsAvailable(int[] loc)
        {
            availableCells.Add(new KeyValuePair<int[], int>(loc, 1));
        }
        public void SetCellAsUnavailable(int[] loc)
        {
            availableCells.Remove(loc);
        }
    }
}
