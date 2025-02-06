using System;
using System.Collections.Generic;

public static class AIController
{
    public static (int row, int col) FindNextMove(GameManager.PlayerType[,] board)
    {
        // TODO: board의 내용을 보고 다음 수를 계산 후 반환
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == GameManager.PlayerType.None)
                {
                    // AI가 이길 수 있는 경우 공격
                    board[row, col] = GameManager.PlayerType.PlayerB;
                    if (CheckResult(GameManager.PlayerType.PlayerB, board))
                    {
                        board[row, col] = GameManager.PlayerType.None;
                        return (row, col);
                    }
                    board[row, col] = GameManager.PlayerType.None;
                    
                    // 플레이어가 이길 경우 방어
                    board[row, col] = GameManager.PlayerType.PlayerA;
                    if (CheckResult(GameManager.PlayerType.PlayerA, board))
                    {
                        board[row, col] = GameManager.PlayerType.None;
                        return (row, col);
                    }
                    board[row, col] = GameManager.PlayerType.None;
                }
            }
        }
        
        // 중앙이 가장 강력한 수
        if (board[1, 1] == GameManager.PlayerType.None) return (1, 1);
        
        // 랜덤 반환
        List<(int,int)> emptyList = new List<(int,int)>();
        
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == GameManager.PlayerType.None)
                {
                    emptyList.Add((row, col));
                }
            }
        }

        return emptyList.Count > 0 ? emptyList[new Random().Next(emptyList.Count)] : (0, 0);
    }
    
    private static bool CheckResult(GameManager.PlayerType playerType, GameManager.PlayerType[,] board)
    {
        // 가로로 마커가 일치하는지 확인
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType && board[row, 1] == playerType && board[row, 2] == playerType)
            {
                return true;
            }

        }
        
        // 세로로 마커가 일치하는지 확인
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType && board[1, col] == playerType && board[2, col] == playerType)
            {
                return true;
            }
        }
        
        // 대각선으로 마커가 일치하는지 확인
        if (board[0, 0] == playerType && board[1, 1] == playerType && board[2, 2] == playerType)
        {
            return true;
        }

        if (board[0, 2] == playerType && board[1, 1] == playerType && board[2, 0] == playerType)
        {
            return true;
        }

        return false;
    }
    
    
    
}