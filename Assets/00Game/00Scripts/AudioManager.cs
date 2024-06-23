using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------Audio Source------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------Audio Clip------")]
    public AudioClip backGround;
    public AudioClip playerAttack;
    public AudioClip playerJump;
    public AudioClip playerSlide;
    public AudioClip playerDefend;
    private bool mute;
    private void Start()
    {
        mute = false;
        musicSource.clip = backGround;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void changeMute() {
        mute = !mute;
        if(mute) {
            AudioListener.volume = 0;
        } else {
            AudioListener.volume = 1;
        }
    }
}
