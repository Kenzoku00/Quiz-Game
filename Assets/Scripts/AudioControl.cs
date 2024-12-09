using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public void ToggleSound()
    {
        AudioManager.Instance.ToggleBGM();
        AudioManager.Instance.ToggleSFX();
    }
}
