using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicMenu : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    
    private float musicFloat = 0.25f;
    private int firstPlayInt = 0;
    private AudioSource audioSource;
    [SerializeField] private AudioClip musicClip;
    private bool isPlayingMusic = false;
    public Button button;
    private ColorBlock colorBlock;
    public Color playingColor;
    public Color notPlayingColor;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = musicClip;
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        colorBlock = button.colors;
        colorBlock.normalColor = playingColor;

        if (firstPlayInt==0)
        {
            musicFloat = 0.25f;
            
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            
        }
        audioSource.volume = musicFloat;
        audioSource.Play();
        isPlayingMusic = true;
    }
    public void PausePlayMusic()
    {
        if (isPlayingMusic)
        {
            audioSource.Pause();
            isPlayingMusic=false;
            colorBlock.normalColor = notPlayingColor;
        } else
        {
            audioSource.UnPause();
            isPlayingMusic = true;
            colorBlock.normalColor = playingColor;
        }
        
    }
    
}
