using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(AudioSource))]
public class SwitchController : MonoBehaviour
{
    [SerializeField] private Image handleImage;
    [SerializeField] private AudioClip clickSound;
    
    public delegate void OnSwitchChangeDelegate(bool isOn);
    public OnSwitchChangeDelegate OnSwitchChange;
    
    private Image _backgroundImage;
    private RectTransform _handleRectTransform;
    private AudioSource _audioSource;
    
    private bool _isOn;
    
    private static readonly Color32 OffColor = new Color32(170, 170, 170, 255);
    private static readonly Color32 OnColor = new Color32(90,90, 90, 255);

    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();
        _handleRectTransform = handleImage.GetComponent<RectTransform>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetOn(false);
    }

    private void SetOn(bool isOn)
    {
        if (isOn)
        {
            _handleRectTransform.DOAnchorPosX(14, 0.2f);
            _backgroundImage.DOBlendableColor(OnColor, 0.2f);
        }
        else
        {
            _handleRectTransform.DOAnchorPosX(-14, 0.2f);
            _backgroundImage.DOBlendableColor(OffColor, 0.2f);
        }
        
        OnSwitchChange?.Invoke(isOn);
        _isOn = isOn;
    }
    
    

    public void OnClickSwitch()
    {
        // 효과음 재생
        if (clickSound != null) _audioSource.PlayOneShot(clickSound);
        
        SetOn(!_isOn);
        
    }
}
