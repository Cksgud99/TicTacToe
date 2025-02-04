using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class PopupPanelController : Singleton<PopupPanelController>
{
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private TMP_Text confirmButtonText;
    [SerializeField] private Button confirmButton;
    
    [SerializeField] private RectTransform panelRectTransform;
    
    private CanvasGroup _canvasGroup;
    
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        ClosePanel(false);
    }

    public void ShowPanel(string content, string confirmButtonText, bool animation, Action confirmAction)
    {
        gameObject.SetActive(true);
        
        // 애니메이션을 위한 초기화
        _canvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        if (animation)
        {
            _canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.OutBack);
            panelRectTransform.DOScale(1f, 0.3f);
        }
        else
        {
            _canvasGroup.alpha = 1;
            panelRectTransform.localScale = Vector3.one;
        }
        
        contentText.text = content;
        this.confirmButtonText.text = confirmButtonText;
        confirmButton.onClick.AddListener(() =>
        {
            confirmAction();
            ClosePanel(true);
        });
    }
    
    public void ClosePanel(bool animation)
    {
        if (animation)
        {
            _canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.InBack);
            panelRectTransform.DOScale(0f, 0.3f).OnComplete(() =>
            {
                contentText.text = "";
                this.confirmButtonText.text = "";
                confirmButton.onClick.RemoveAllListeners();
        
                gameObject.SetActive(false);
            });
        }
        else
        {
            contentText.text = "";
            this.confirmButtonText.text = "";
            confirmButton.onClick.RemoveAllListeners();
        
            gameObject.SetActive(false);
        }
    }
}