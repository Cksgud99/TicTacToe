using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private PanelController startPanelController;

    public enum PanelType { StartPanel, WinPanel, DrawPanel ,LosePanel }

    private PanelController _currentPanelController;
    
    /// <summary>
    /// 표시할 패널 정보 전달하는 함수
    /// </summary>
    /// <param name="panelType"></param>
    public void ShowPanel(PanelType panelType)
    {
        switch (panelType)
        {
            case PanelType.StartPanel:
                ShowPanelController(startPanelController);
                break;
            case PanelType.WinPanel:
                break;
            case PanelType.DrawPanel:
                break;
            case PanelType.LosePanel:
                break;
        }
    }

    
    /// <summary>
    /// 패널을 표시하는 함수
    /// 기존 패널은 Hide, 새로운 패널은 Show
    /// </summary>
    /// <param name="panelController"></param>
    private void ShowPanelController(PanelController panelController)
    {
        if (_currentPanelController != null)
        {
            _currentPanelController.Hide();
        }

        panelController.Show();
        _currentPanelController = panelController;
    }
    
}
