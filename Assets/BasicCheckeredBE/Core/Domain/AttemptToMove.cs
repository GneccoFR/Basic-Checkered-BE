using System.Collections.Generic;

namespace BasicCheckeredBE.Core.Domain
{
    public class AttemptToMove
    {
        public bool Success;
        public string Message;
        public List<BoardSquare> UpdatedBoardSquares;

        public AttemptToMove(bool success, string message, List<BoardSquare> updatedBoardSquares)
        {
            Success = success;
            Message = message;
            UpdatedBoardSquares = updatedBoardSquares;
        }
    }
}