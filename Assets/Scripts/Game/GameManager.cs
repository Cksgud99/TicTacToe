using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private BlockController blockController;
    
    private enum PlayerType { None, PlayerA, PlayerB }
    private PlayerType[,] _board;
    
    private enum TurnType { PlayerA, PlayerB }

    private enum GameResult
    {
        None,   // 게임 진행 중
        Win,    // 플레이어 승
        Lose,   // 플레이어 패
        Draw    // 비김
    }

    private void Start()
    {
        // 게임 초기화
        InitGame();
        
    }
    
    /// <summary>
    /// 게임 초기화 함수
    /// </summary>
    public void InitGame()
    {
        // _board 초기화
        _board = new PlayerType[3, 3];
        
        // Block 초기화
        blockController.InitBlocks();
    }

    public void StartGame()
    {
        SetTurn(TurnType.PlayerA);
    }

    private void EndGame()
    {
        
    }

    /// <summary>
    ///  _board에 새로운 값을 할당하는 함순
    /// </summary>
    /// <param name="playerType">할당하고자 하는 플레이어 타입</param>
    /// <param name="row">Row</param>
    /// <param name="col">Col</param>
    /// <returns>False가 반환되면 할당할 수 없음, True는 할당이 완료됨</returns>
    private bool SetNewBoardValue(PlayerType playerType, int row, int col)
    {
        if (playerType == PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        
        else if (playerType == PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }

        return false;
    }

    private void SetTurn(TurnType turnType)
    {
        switch (turnType)
        {
            case TurnType.PlayerA:
                Debug.Log("Player A turn");

                blockController.OnBlockClickedDelegate = (row, col) =>
                {
                    SetNewBoardValue(PlayerType.PlayerA, row, col);
                };
                
                break;
            case TurnType.PlayerB:
                // TODO: AI에게 입력 받기
                
                break;
        }
        // 게임 결과 확인

        switch (CheckGameResult())
        {
            case GameResult.Win:
                // TODO: 승리 결과창 표시
                break;
            case GameResult.Lose:
                // TODO: 패배 결과창 표시
                break;
            case GameResult.Draw:
                // TODO: 비김 결과창 표시
                break;
            case GameResult.None:
                // 게임이 종료되지 않았다면, 턴 변경
                var nextTurn = turnType == TurnType.PlayerA ? TurnType.PlayerB : TurnType.PlayerA;
                SetTurn(nextTurn);
                break;
        }
        

    }

    /// <summary>
    /// 게임 결과 확인 함수
    /// </summary>
    /// <returns></returns>
    private GameResult CheckGameResult()
    {
        if(CheckGameWin(PlayerType.PlayerA)) return GameResult.Win;
        if(CheckGameWin(PlayerType.PlayerB)) return GameResult.Lose;
        if(IsAllBlocksPlaced()) return GameResult.Draw;
        
        return GameResult.None;
    }
    
    /// <summary>
    /// 모든 마커가 보드에 배치 되었는지 확인하는 함수
    /// </summary>
    /// <returns>True: 모두 배치</returns>
    private bool IsAllBlocksPlaced()
    {
        for (var row = 0; row < _board.GetLength(0); row++)
        {
            for (var col = 0; col < _board.GetLength(1); col++)
            {
                if (_board[row, col] == PlayerType.None) return false;
            }
        }
        return true;
    }
    
    // 게임의 승패를 판단하는 함수
    private bool CheckGameWin(PlayerType playerType)
    {
        // 가로로 마커가 일치하는지 확인
        for (var row = 0; row < _board.GetLength(0); row++)
        {
            if (_board[row, 0] == playerType && _board[row, 1] == playerType && _board[row, 2] == playerType) return true;

        }
        
        // 세로로 마커가 일치하는지 확인
        for (var col = 0; col < _board.GetLength(1); col++)
        {
            if (_board[0, col] == playerType && _board[1, col] == playerType && _board[2, col] == playerType) return true;
        }
        
        // 대각선으로 마커가 일치하는지 확인
        if (_board[0, 0] == playerType && _board[1, 1] == playerType && _board[2, 2] == playerType) return true;
        if (_board[0, 2] == playerType && _board[1, 1] == playerType && _board[2, 0] == playerType) return true;

        return false;
    }
}