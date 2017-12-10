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
        public List<Piece> pieces = new List<Piece>();
        private static int ID = 0;
        public int id;

        public Player(string name)
        {
            ID += 1;
            id = ID;
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

        public bool RotatePiece(string pieceName, double degrees, out string actualpiecename)
        {
            bool rotated = false;
            actualpiecename = "N/A";
            foreach(var p in pieces)
            {
                if (p.name.ToLower() == pieceName)
                {
                    p.RotatePiece(degrees);
                    actualpiecename = p.name;
                    rotated = true;
                }
            }
            return (rotated);
        }

        public bool FlipPiece(string pieceName, out string actualpiecename)
        {
            bool flipped = false;
            actualpiecename = "N/A";
            foreach (var p in pieces)
            {
                if (p.name.ToLower() == pieceName)
                {
                    p.FlipPiece();
                    actualpiecename = p.name;
                    flipped = true;
                }
            }
            return (flipped);
        }

        public void SetCellAsAvailable(int[] loc)
        {
            if(availableCells.ContainsKey(loc) == false) availableCells.Add(new KeyValuePair<int[], int>(loc, 1));
        }
        public void SetCellAsUnavailable(int[] loc)
        {
            availableCells.Remove(loc);
        }
    }
}
