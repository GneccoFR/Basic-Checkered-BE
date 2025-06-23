using System.Collections.Generic;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Networking;
using BasicCheckeredBE.Networking.DTOs;
using BasicCheckeredBE.Repositories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace BasicCheckeredBE.Controllers
{
    public class GameBoardController /*: IController*/
    {
        /*
        private GetNewBoardService _getNewBoardService;
        private AttemptToMoveService _attemptToMoveService;
        
        public void Initialize()
        {
            _getNewBoardService = new GetNewBoardService();
            _attemptToMoveService = new AttemptToMoveService();
        }
        */
        
        public async UniTask<BoardSquare[,]> GetNewBoard(int boardSize = 8)
        {
            // Simulate fetching a new board
            await UniTask.Delay(500); // Simulate network delay
            
            //SquareDTO[,] board = new SquareDTO[boardSize, boardSize];
            BoardSquare[,] board = new BoardSquare[boardSize, boardSize];
            
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    board[x, y] = new BoardSquare(new Piece(GlobalFields.PieceType.Pawn, GlobalFields.PlayerType.Player1), new Vector2(x,y));
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player1 Pawn");
                }
            }

            for (int y = 2; y < 6; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    board[x, y] = new BoardSquare(new Piece(GlobalFields.PieceType.None, GlobalFields.PlayerType.None), new Vector2(x,y));
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to None");
                }
            }
            
            for (int y = 6; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    board[x, y] = new BoardSquare(new Piece(GlobalFields.PieceType.Pawn, GlobalFields.PlayerType.Player2), new Vector2(x,y));
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player2 Pawn");
                }
            }

            return board;
        }

        public async UniTask<AttemptToMove> AttemptToMove(Piece piece, BoardSquare originalSquare, BoardSquare targetSquare)
        {
            await UniTask.Delay(250); // Simulate processing time
            
            Debug.Log($"GATEWAY: Attempting move: {piece} from [{originalSquare.Coordinates.X}, {originalSquare.Coordinates.Y}, ({originalSquare.Piece.PieceType})] to [{targetSquare.Coordinates.X}, {targetSquare.Coordinates.Y}, ({targetSquare.Piece.PieceType})]");
            
            //Evaluation Block: Is the target square occupied? 
            if (targetSquare.Piece.PieceType != GlobalFields.PieceType.None)
            {
                var invalidMove = new List<BoardSquare>();    
                
                invalidMove.Add(new BoardSquare(piece,new Vector2(originalSquare.Coordinates.X, originalSquare.Coordinates.Y)));

                return new AttemptToMove(false,"Invalid move! The target square is occupied!", invalidMove);
            }
            
            //Evaluation Block: Is the piece moving to a non-adjacent square or is jumping over an enemy piece?
            if (Mathf.Abs(originalSquare.Coordinates.X - targetSquare.Coordinates.X) > 1 || Mathf.Abs(originalSquare.Coordinates.Y - targetSquare.Coordinates.Y) > 1)
            {
                var invalidMove = new List<BoardSquare>();
                
                invalidMove.Add(new BoardSquare(piece,new Vector2(originalSquare.Coordinates.X, originalSquare.Coordinates.Y)));
                
                return new AttemptToMove(false,"Invalid move! The piece can only move to adjacent squares!", invalidMove);
            }
            
            //Sending move block
            
            var boardSquaresToUpdate = new List<BoardSquare>();
            
            boardSquaresToUpdate.Add(new BoardSquare(new Piece(GlobalFields.PieceType.None, GlobalFields.PlayerType.None), new Vector2(originalSquare.Coordinates.X, originalSquare.Coordinates.Y)));
            boardSquaresToUpdate.Add(new BoardSquare(piece, new Vector2(targetSquare.Coordinates.X, targetSquare.Coordinates.Y)));
            
            Debug.Log($"GATEWAY: Sending move: {piece} from [{boardSquaresToUpdate[0].Coordinates.X}, {boardSquaresToUpdate[0].Coordinates.Y}] to [{boardSquaresToUpdate[1].Coordinates.X}, {boardSquaresToUpdate[1].Coordinates.Y}]");
            return new AttemptToMove(true,"Move Successful!", boardSquaresToUpdate);
        }
    }
    
    /*
    public class AttemptToMoveService
    {

    }

    public class GetNewBoardService
    {

    }

    public interface IController
    {
        void Initialize();
    }
    */
}