using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blokus
{
    public class Piece
    {
        private int orientation = 0;
        public string name { get; set; }
        public int count { get; set; } // this will indicate how many of these pieces a player has
        public List<int[]>
            points,
            corners;
        public int id;
        // min/max coordinates of this piece
        public int
            minX,
            maxX,
            minY,
            maxY;
        // min/max coordinates of all pieces
        public static int
            allMinX,
            allMaxX,
            allMinY,
            allMaxY;

        // an array containing which of the 8 orientations (rotate and flips) are unique.
        public bool[] validOrientations = new bool[8];

        /*
        public Piece(string name, List<int[]> points, List<int[]> corners, int count = 1)
        {
            this.name = name;
            this.points = points;
            this.corners = corners;
            this.count = count;
        }
        */

        public Piece(string name, List<int[]> points, int count = 1, int orientation = 0, bool isClone = false)
        {
            this.name = name;
            this.points = new List<int[]>();
            foreach(var point in points)
            {
                this.points.Add(new int[] { point[0], point[1] });
            }
            this.corners = GetCorners(points);
            this.count = count;
            this.orientation = orientation;

            // get max positions of the piece
            ResetContainingBoxValues();

            // check which of the 8 orientations are unique.
            if(isClone==false) setUniqueOrientations();
        }

        public Piece Clone(int orientation)
        {
            Piece newpiece = new Piece(name, points, 1, orientation, true);

            return (newpiece);
        }

        private void ResetContainingBoxValues()
        {
            int[] coords = getContainingBox(points);
            minX = coords[0];
            maxX = coords[1];
            minY = coords[2];
            maxY = coords[3];
        }
        
        private void setUniqueOrientations()
        {
            List<Piece> orientations = new List<Piece>();

            // create orientation clones
            orientations.Add(Clone(0));

            RotatePiece(90.0);
            orientations.Add(Clone(1));

            RotatePiece(90.0);
            orientations.Add(Clone(2));

            RotatePiece(90.0);
            orientations.Add(Clone(3));

            RotatePiece(90.0);
            FlipPiece();
            orientations.Add(Clone(4));

            RotatePiece(90.0);
            orientations.Add(Clone(5));

            RotatePiece(90.0);
            orientations.Add(Clone(6));

            RotatePiece(90.0);
            orientations.Add(Clone(7));
            // return piece to original orientation
            RotatePiece(90.0);
            FlipPiece();

            // initialize values to 1
            for (int i = 0; i < validOrientations.Length; i++)
            {
                validOrientations[i] = true;
            }
            // change values to 0 if the orientation is a duplicate
            for (int i = 0; i<orientations.Count; i++)
            {
                for(int j = i+1; j<orientations.Count; j++)
                {
                    if (validOrientations[i]==true // short circuiting. If orientation[i]=0, then it is a duplicate, and checking duplicate j has already been done before
                            && orientations[i].Equals(orientations[j]) == true)
                        validOrientations[j] = false;
                }
            }
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
                for(int j=box.GetLength(1)-1; j>=0; j--)
                {
                    if (box[i, j] == 0) Console.Write("-");
                    if (box[i, j] == 1) Console.Write("X");
                    if (box[i, j] == 2) Console.Write("C");
                }
                Console.Write("\n");
            }
        }

        private int[] getContainingBox(List<int[]> points)
        {
            int[] res = new int[4];
            // minx, maxx, miny, maxy will be the min/max positions of the entire piece. Initialized with dummy values
            int minx = 20,
                maxx = -20,
                miny = 20,
                maxy = -20;

            foreach(var coords in points)
            {
                int x = coords[0];
                int y = coords[1];

                // sets min/max coordinates for this piece
                if (x < minx) minx = x;
                if (x > maxx) maxx = x;
                if (y < miny) miny = y;
                if (y > maxy) maxy = y;

                // set min/max coordinates for all pieces
                if (x < allMinX) allMinX = x;
                if (x > allMaxX) allMaxX = x;
                if (y < allMinY) allMinY = y;
                if (y > allMaxY) allMaxY = y;
            }

            // put the values in an array and return it
            res[0] = minx;
            res[1] = maxx;
            res[2] = miny;
            res[3] = maxy;
            return (res);
        }

        public List<int[]> GetCorners(List<int[]> points)
        {
            int center = 6; // 6 = center point 12 divided by 2
            int[,] box = new int[center*2, center*2];

            // find the corners
            foreach (var p in points)
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

            ResetContainingBoxValues();
        }

        public void FlipPiece() // flips along the y axis
        {
            foreach(int[] p in points)
            {
                p[1] = -p[1];
            }
            foreach(int[] c in corners)
            {
                c[1] = -c[1];
            }

            ResetContainingBoxValues();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var comparedPiece = (Piece)obj;

            // pieces to compare do not have equal number of points, so they cannot be equal
            if (points.Count != comparedPiece.points.Count) return (false);

            // compare all points in the piece against all of the other piece's points
            for(int i=0; i<points.Count; i++)
            {
                bool pointsAreInComparedPiecePoints = false;
                for(int j= 0; j < comparedPiece.points.Count; j++)
                {
                    if ((points[i])[0]-minX == (comparedPiece.points[j])[0]-comparedPiece.minX && (points[i])[1]-minY == (comparedPiece.points[j])[1]-comparedPiece.minY)
                        pointsAreInComparedPiecePoints = true;
                }
                if (pointsAreInComparedPiecePoints == false) return (false); // if any point is not contained the the other piece, then we know they aren't equal
            }
            return (true);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
        /*
        public bool Equals(Piece obj)
        {

            if (obj == null || GetType() != obj.GetType()) return false;
            
            var comparedPiece = (Piece)obj;

            // pieces to compare do not have equal number of points, so they cannot be equal
            if (points.Count != comparedPiece.points.Count) return (false);

            for(int i = 0; i<points.Count; i++)
            {
                if ((points[i])[0] != (comparedPiece.points[i])[0] || (points[i])[1] != (comparedPiece.points[i])[1]) return (false);
            }
            //if (points.SequenceEqual(comparedPiece.points)) return (true);
            //return (points == ((Piece)obj).points);
            return (true);
        }
        */
        }
    }
