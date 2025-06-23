using BasicCheckeredBE.Networking;

namespace BasicCheckeredBE.Core.Domain
{
    public class Piece
    {
        public GlobalFields.PieceType PieceType;
        public GlobalFields.PlayerType Owner;
        
        public Piece(GlobalFields.PieceType piece, GlobalFields.PlayerType owner)
        {
            PieceType = piece;
            Owner = owner;
        }
    }
}