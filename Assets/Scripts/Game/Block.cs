using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
    [SerializeField] private Sprite oSpirte;
    [SerializeField] private Sprite xSpirte;
    [SerializeField] private SpriteRenderer markerSpriteRenderer;

    public enum MarkerType { None, O, X }

    public delegate void OnBlockClicked(int index);
    public OnBlockClicked onBlockClicked;
    
    private int _blockIndex;

    /// <summary>
    /// Block 초기화 함수
    /// </summary>
    /// <param name="blockIndex">Block 인덱스</param>
    /// <param name="onBlockClicked">Block 터치 이벤트</param>
    public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked)
    {
        _blockIndex = blockIndex;
        SetMarker(MarkerType.None);
        this.onBlockClicked = onBlockClicked;
    }
    
    /// <summary>
    /// 어떤 마커를 표시할 지 전달하는 함수
    /// </summary>
    /// <param name="markerType">마커 타입</param>
    public void SetMarker(MarkerType markerType)
    {
        switch (markerType)
        {
            case MarkerType.O:
                markerSpriteRenderer.sprite = oSpirte;
                break;
            case MarkerType.X:
                markerSpriteRenderer.sprite = xSpirte;
                break;
            case MarkerType.None:
                markerSpriteRenderer.sprite = null;
                break;
        }
    }

    private void OnMouseUpAsButton()
    {
        onBlockClicked?.Invoke(_blockIndex);
    }
}
