using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blokus
{
    public class Piece
    {
        public string name { get; set; }
        //private int size;
        private List<int[]> points;
        private List<int[]> corners;

        public Piece(string name, List<int[]> points, List<int[]> corners)
        {
            this.name = name;
            this.points = points;
            this.corners = corners;
        }
        public Piece(string name, List<int[]> points)
        {
            this.name = name;
            this.points = points;
            this.corners = GetCorners(points);
        }

        public void PrintPiece()
        {
            int[,] box = new int[12, 12]; // creates an 12x12 box, which will determine where the block pieces are
            int center = 6;

            foreach(var p in points)
            {
                box[p[0]+center, p[1]+center] = 1; // marks that the piece has a block at that location. Adds center because pieces can have negative coordinates, and the +center will center it to the box.
            }
            foreach (var c in corners)
            {
                box[c[0] + center, c[1] + center] = 2; // marks that the piece has a corner at that location
            }

            for(int i=0; i<box.GetLength(0); i++)
            {
                for(int j=0; j<box.GetLength(1); j++)
                {
                    if (box[i, j] == 0) Console.Write("-");
                    if (box[i, j] == 1) Console.Write("X");
                    if (box[i, j] == 2) Console.Write("C");
                }
                Console.Write("\n");
            }
        }

        public List<int[]> GetCorners(List<int[]> points)
        {
            int[,] box = new int[12, 12];
            int center = 6; // 6 = center point 12 divided by 2

            // find the corners
            foreach(var p in points)
            {
                int x = p[0] + center,
                    y = p[1] + center;
                box[x, y] = 1;
                if (box[x - 1, y + 1] == 0) box[x - 1, y + 1] = 2;
                if (box[x + 1, y + 1] == 0) box[x + 1, y + 1] = 2;
                if (box[x + 1, y - 1] == 0) box[x + 1, y - 1] = 2;
                if (box[x - 1, y - 1] == 0) box[x - 1, y - 1] = 2;
            }
            // set adjacent points to 0 if they were incorrectly considered corner points earlier
            foreach (var p in points)
            {
                int x = p[0] + center,
                    y = p[1] + center;
                if (box[x - 1, y] == 2) box[x - 1, y] = 0;
                if (box[x + 1, y] == 2) box[x + 1, y] = 0;
                if (box[x, y - 1] == 2) box[x, y - 1] = 0;
                if (box[x, y + 1] == 2) box[x, y + 1] = 0;
            }

            List<int[]> res = new List<int[]>();
            for(int i=0; i<box.GetLength(0); i++)
            {
                for(int j=0; j<box.GetLength(1); j++)
                {
                    if (box[i, j] == 2) res.Add(new int[] { i-center, j-center});
                }
            }

            return (res);
        }

        public void RotatePiece(double degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            foreach(int[] p in points)
            {
                double newx = (double)p[0] * Math.Cos(angle) - (double)p[1] * Math.Sin(angle);
                double newy = (double)p[0] * Math.Sin(angle) + (double)p[1] * Math.Cos(angle);
                // round the new coordinates to an integer. Doing a (int)newx would sometimes be wrong because it rounds to the even number (0.9999 -> 0).
                p[0] = newx < 0 ? (int)(newx - 0.5) : (int)(newx + 0.5);
                p[1] = newy < 0 ? (int)(newy - 0.5) : (int)(newy + 0.5);
            }
            foreach (int[] c in corners)
            {
                double newx = (double)c[0] * Math.Cos(angle) - (double)c[1] * Math.Sin(angle);
                double newy = (double)c[0] * Math.Sin(angle) + (double)c[1] * Math.Cos(angle);
                // round the new coordinates to an integer. Doing a (int)newx would sometimes be wrong because it rounds to the even number (0.9999 -> 0)
                c[0] = newx < 0 ? (int)(newx - 0.5) : (int)(newx + 0.5);
                c[1] = newy < 0 ? (int)(newy - 0.5) : (int)(newy + 0.5);
            }
        }

        public void FlipPiece()
        {
            foreach(int[] p in points)
            {
                p[1] = -p[1];
            }
            foreach(int[] c in corners)
            {
                c[1] = -c[1];
            }
        }
    }
}
