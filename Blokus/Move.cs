using System.Collections.Generic;

namespace Blokus
{
    public class Move// : IEquatable<Move>
    {
        public int[] PlacePieceAt = new int[2];
        // this will be a list of which part of the Piece can fit. i.e. which x in Piece.points[x] can fit in PlacePieceAt
        public List<int> validCenters = new List<int>();
        public Piece piece { get; set; }
        public int playerID { get; set; }

        public Move(int playerID, Piece piece, int[] coord, List<int> validCenters)
        {
            this.playerID = playerID;
            this.piece = piece;
            this.PlacePieceAt = coord;
            this.validCenters = validCenters;
        }

        /*
        public override int GetHashCode()
        {
            return piece.id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            
            return (piece.Equals(((Move)obj).piece) && PlacePieceAt[0]==((Move)obj).PlacePieceAt[0] && PlacePieceAt[1] == ((Move)obj).PlacePieceAt[1]);
        }

        public bool Equals(Move obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            
            return (piece.Equals(obj.piece) && PlacePieceAt[0]==obj.PlacePieceAt[0] && PlacePieceAt[1] == obj.PlacePieceAt[1]);
        }
        */
    }
}
