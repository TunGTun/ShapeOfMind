using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMuteController : MonoBehaviour
{
    public Image volumeImage;   
    public Sprite unmuteSprite;  
    public Sprite muteSprite;     

    private bool isMuted = false; 

    private void OnEnable()
    {
        isMuted = AudioManager.Instance.IsMuted;
        if (isMuted)
        {
            volumeImage.sprite = muteSprite;
            AudioListener.volume = 0f;  
        }
        else
        {
            volumeImage.sprite = unmuteSprite;
            AudioListener.volume = 1f;  
        }
    }


    public void ToggleMute()
    {
        isMuted = !isMuted; 
        AudioManager.Instance.IsMuted = isMuted;
        if (isMuted)
        {
            volumeImage.sprite = muteSprite;
            AudioListener.volume = 0f;  
        }
        else
        {
            volumeImage.sprite = unmuteSprite;
            AudioListener.volume = 1f;  
        }
    }
}