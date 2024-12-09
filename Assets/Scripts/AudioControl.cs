using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private Sprite soundOnSprite;   
    [SerializeField] private Sprite soundOffSprite;  
    [SerializeField] private Image buttonImage;      

    private bool isMuted = false;  

    public void ToggleSound()
    {
        isMuted = !isMuted;

        AudioManager.Instance.MuteBGM(isMuted);
        AudioManager.Instance.MuteSFX(isMuted);

        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (isMuted)
        {
            buttonImage.sprite = soundOffSprite; 
        }
        else
        {
            buttonImage.sprite = soundOnSprite;  
        }
    }
}
