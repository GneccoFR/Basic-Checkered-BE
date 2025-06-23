using BasicCheckeredBE.Controllers;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Networking.DTOs;
using BasicCheckeredBE.Repositories;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Networking
{
    public class GameRules
    {
        private GameBoardController _gameBoardController;
        private MemoryRepositoryManager _repositoryManager;
        
        public GameRules(MemoryRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        
        public async UniTask Initialize()
        {
            _gameBoardController = new GameBoardController();
        }
        
        public async UniTask<BoardSquare[,]> GetNewBoard()
        {
            var gameBoard = await _gameBoardController.GetNewBoard();
            _repositoryManager.GameBoardRepository.LoadBoard(gameBoard);
            return await _repositoryManager.GameBoardRepository.GetCurrentBoard();
        }
        
        public async UniTask<AttemptToMove> AttemptToMove(Piece piece, BoardSquare originalSquare, BoardSquare targetSquare)
        {
            return await _gameBoardController.AttemptToMove(piece, originalSquare, targetSquare);
        }
    }
}
