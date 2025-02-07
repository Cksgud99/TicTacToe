using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject confirmPanel;
    
    private BlockController _blockController;
    private GameUIController _gameUIController;
    private Canvas _canvas;
    
    public enum PlayerType { None, PlayerA, PlayerB }
    private PlayerType[,] _board;
    
    private enum TurnType { PlayerA, PlayerB }

    private enum GameResult
    {
        None,   // 게임 진행 중
        Win,    // 플레이어 승
        Lose,   // 플레이어 패
        Draw    // 비김
    }

    public enum GameType { SinglePlay, DualPlay }
    
    public void ChangeToGameScene(GameType gameType)
    {
        SceneManager.LoadScene("Game");
    }

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenSettingsPanel()
    {
        if (_canvas != null)
        {
            var settingsPanelObject = Instantiate(settingsPanel, _canvas.transform);
            settingsPanelObject.GetComponent<PanelController>().Show();
        }
    }

    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClick onConfirmButtonClick)
    {
        var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
        confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClick);
    }
    
    /// <summary>
    /// 게임 시작
    /// </summary>
    private void StartGame()
    {
        // _board 초기화
        _board = new PlayerType[3, 3];
        
        // Block 초기화
        _blockController.InitBlocks();
        
        // Game UI 초기화
        _gameUIController.SetGameUIMode(GameUIController.GameUIMode.Init);
        
        // 턴 시작
        SetTurn(TurnType.PlayerA);
    }

    /// <summary>
    /// 게임 오버시 호출되는 함수
    /// gameResult에 따라 결과 출력
    /// </summary>
    /// <param name="gameResult">win, lose, draw</param>
    private void EndGame(GameResult gameResult)
    {
        _gameUIController.SetGameUIMode(GameUIController.GameUIMode.GameOver);
        _blockController.OnBlockClickedDelegate = null;
        
        // TODO: 나중에 구현!
        switch (gameResult)
        {
            case GameResult.Win:
                break;
            case GameResult.Lose:
                break;
            case GameResult.Draw:
                break;
        }
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
        if (_board[row, col] != PlayerType.None) return false;
        
        if (playerType == PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            _blockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        
        else if (playerType == PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            _blockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }

        return false;
    }

    private void SetTurn(TurnType turnType)
    {
        switch (turnType)
        {
            case TurnType.PlayerA:
                _gameUIController.SetGameUIMode(GameUIController.GameUIMode.TurnA);
                
                _blockController.OnBlockClickedDelegate = null;
                _blockController.OnBlockClickedDelegate = (row, col) =>
                {
                    if (SetNewBoardValue(PlayerType.PlayerA, row, col))
                    {
                        var gameResult = CheckGameResult();
                        
                        if (gameResult == GameResult.None) SetTurn(TurnType.PlayerB);
                        else EndGame(gameResult);
                    }

                    else
                    {
                        // TODO: 이미 있는 곳을 터치했을 때 처리
                    }
                };
                break;
            
            case TurnType.PlayerB:
                _gameUIController.SetGameUIMode(GameUIController.GameUIMode.TurnB);

                var result = AIController.FindNextMove(_board);
                
                if (SetNewBoardValue(PlayerType.PlayerB, result.row, result.col))
                {
                    var gameResult = CheckGameResult();
                        
                    if (gameResult == GameResult.None) SetTurn(TurnType.PlayerA);
                    else EndGame(gameResult);
                }

                else
                {
                    // TODO: 이미 있는 곳을 터치했을 때 처리
                }

                break;
        }
    }

    /// <summary>
    /// 게임 결과 확인 함수
    /// </summary>
    /// <returns></returns>
    private GameResult CheckGameResult()
    {
        if(MinmaxAIController.CheckGameWin(PlayerType.PlayerA, _board)) return GameResult.Win;
        if(MinmaxAIController.CheckGameWin(PlayerType.PlayerB, _board)) return GameResult.Lose;
        if(MinmaxAIController.IsAllBlocksPlaced(_board)) return GameResult.Draw;
        
        return GameResult.None;
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            _blockController = GameObject.FindObjectOfType<BlockController>();
            _gameUIController = GameObject.FindObjectOfType<GameUIController>();
            
            // 게임 시작
            StartGame();
        }
        
        _canvas = GameObject.FindObjectOfType<Canvas>();
    }
}