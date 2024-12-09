using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip defaultBGM;

    [SerializeField] private AudioClip wlSound;

    private bool isBGMOn = true;
    private bool isSFXOn = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (defaultBGM != null)
        {
            PlayBGM(defaultBGM);
        }
    }

    private void PlayBGM(AudioClip clip)
    {
        if (bgmSource != null && clip != null)
        {
            bgmSource.clip = clip;
            if (isBGMOn)
            {
                bgmSource.Play();
            }
        }
    }

    public void ToggleBGM()
    {
        isBGMOn = !isBGMOn;

        if (bgmSource != null)
        {
            if (isBGMOn)
            {
                bgmSource.Play();
            }
            else
            {
                bgmSource.Pause();
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null && isSFXOn)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
    }

    public bool IsBGMOn() => isBGMOn;
    public bool IsSFXOn() => isSFXOn;

    public void PlayWLSound()
    {
        if (wlSound !=  null && IsSFXOn())
        {
            PlaySFX(wlSound);
        }
    }

    public void MuteBGM(bool mute)
    {
        if (bgmSource != null)
        {
            if (mute)
            {
                bgmSource.Pause(); 
            }
            else
            {
                bgmSource.UnPause(); 
            }
        }
    }

}
