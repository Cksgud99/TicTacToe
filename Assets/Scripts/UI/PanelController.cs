using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PanelController : MonoBehaviour
{
    public bool IsShow { get; private set; }

    public delegate void OnHide();
    private OnHide _onHideDelegate;

    private RectTransform _rectTransform;
    private Vector2 _hideAnchoredPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _hideAnchoredPosition = _rectTransform.anchoredPosition;
        IsShow = false;
    }

    /// <summary>
    /// Panel 표시 함수
    /// </summary>
    public void Show(OnHide onHideDelegate)
    {
        _onHideDelegate = onHideDelegate;
        _rectTransform.anchoredPosition = Vector2.zero;
        IsShow = true;
    }

    /// <summary>
    /// Panel 숨기기 함수
    /// </summary>
    public void Hide()
    {
        _rectTransform.anchoredPosition = _hideAnchoredPosition;
        IsShow = false;
        _onHideDelegate?.Invoke();
    }
}