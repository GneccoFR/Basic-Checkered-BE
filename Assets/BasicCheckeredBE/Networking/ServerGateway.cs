using BasicCheckeredBE.Networking.DTOs;
using BasicCheckeredBE.Repositories;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Networking
{
    public class ServerGateway : IGateway
    {
        public static ServerGateway Instance { get; } = new ServerGateway();
        //private readonly IJsonFacade _jsonFacade = new JsonFacade();
        private MemoryRepositoryManager _repositoryManager;
        private GameRules _gameRules;
        
        public async UniTask Initialize()
        {
            _repositoryManager = new MemoryRepositoryManager();
            _repositoryManager.Initialize();
            _gameRules = new GameRules(_repositoryManager);
            await _gameRules.Initialize();
        }
        
        public async UniTask<BoardDTO> GetNewBoard()
        {
            return Mapper.ToBoardDTO(await _gameRules.GetNewBoard());
        }
        
        public async UniTask<AttemptToMoveDTO> AttemptToMove(PieceDTO piece, SquareDTO originalSquare, SquareDTO targetSquare)
        {
            var attempt = await _gameRules.AttemptToMove(
                piece.ToPiece(),
                originalSquare.ToBoardSquare(),
                targetSquare.ToBoardSquare()
            );
            return new AttemptToMoveDTO(attempt.Success, attempt.Message, attempt.UpdatedBoardSquares.ToSquareDTOList());
        }
    }
    
    public class GameState
    {
        // Game State Model Instance
        // Game State Data Getters and Setters
    }

    /*
    public interface IJsonFacade
    {
    }

    public class JsonFacade : IJsonFacade
    {
    }
*/
}