using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXClips : MonoBehaviour
{
    //public AudioClip[] SFXClipArray;
    public AudioSource SFXPlayer;

    public void PlaySFX(AudioClip clip)
    {
        SFXPlayer.PlayOneShot(clip);
    }

}
