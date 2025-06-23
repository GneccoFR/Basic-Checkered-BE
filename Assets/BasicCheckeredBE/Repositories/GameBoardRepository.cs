using BasicCheckeredBE.Core.Domain;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Repositories
{
    public class GameBoardRepository
    {
        private BoardSquare[,] _boardCache;

        public void LoadBoard(BoardSquare[,] board)
        {
            _boardCache = board;
        }
        
        public async UniTask<BoardSquare[,]> GetCurrentBoard()
        {
            return _boardCache;
        }
    }
}