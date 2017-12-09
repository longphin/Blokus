using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blokus
{
    public class Move// : IEquatable<Move>
    {
        int playerID { get; set; }
        public Piece piece { get; set; }
        int[] PlacePieceAt = new int[2];

        public Move(int playerID, Piece piece, int[] coord)
        {
            this.playerID = playerID;
            this.piece = piece;
            this.PlacePieceAt = coord;
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
