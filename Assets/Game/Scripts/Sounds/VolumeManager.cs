using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private int firstPlayInt;
    public Slider musicSlider, soundEffectsSlider;
    private float musicFloat, soundEffectsFloat;
    private AudioSource musicsAudio;
    private AudioSource soundEffectsAudio;

    public AudioClip[] musicClip;
    public AudioClip[] soundEffectClip;
    private float timeClip;
    //private int countAudioSourceSFX = 0;

    //private string musicChangeMode = "";
    public enum MusicChangeMode
    {
        Stop, Pause, Play, SelectLevelMusic, SelectLosePanelMusic, SelectMenuMusic
    }

    void Start()
    {


        musicsAudio = gameObject.AddComponent<AudioSource>();
        soundEffectsAudio = gameObject.AddComponent<AudioSource>();
        musicsAudio.volume = 0;
        soundEffectsAudio.volume = 0;


        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            musicFloat = 0.25f;
            soundEffectsFloat = 0.75f;
            musicSlider.value = musicFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicSlider.value = musicFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        musicsAudio.volume = musicSlider.value;

        soundEffectsAudio.volume = soundEffectsSlider.value;

    }

    public void PlaySFX(int item)
    {

        soundEffectsAudio.PlayOneShot(soundEffectClip[item]);

    }

    
    public void MusicMode(MusicChangeMode musicChangeMode)
    {
        switch (musicChangeMode)
        {
            case MusicChangeMode.Stop: StopPlayMusic();
                break;
            case MusicChangeMode.Pause: PausePlayMusic();
                break;
            case MusicChangeMode.Play: StartPlayMusic();
                break;
            case MusicChangeMode.SelectLevelMusic: SelectLevelMusic();
                break;
            case MusicChangeMode.SelectLosePanelMusic: SelectLosePanelMusic();
                break;
            case MusicChangeMode.SelectMenuMusic: SelectMenuMusic();
                break;
            default: Debug.Log("VolumeManager не получил режим воспроизведения музыки уровня!!!!");
                break;
        }
    }
    public void StopPlayMusic()
    {
        musicsAudio.Stop();
    }
    public void PausePlayMusic()
    {
        musicsAudio.Pause();
    }
    public void StartPlayMusic()
    {
        musicsAudio.Play();
    }
    public void SelectLevelMusic()
    {
        StopPlayMusic();
        musicsAudio.clip = musicClip[Random.Range(2, 4)];
        musicsAudio.loop = false;
        timeClip = musicsAudio.clip.length*Time.timeScale;
        StartCoroutine(Wait());

    }
    public void SelectLosePanelMusic()
    {
        StopPlayMusic();
        musicsAudio.clip = musicClip[1];
        musicsAudio.loop = true;
    }
    public void SelectMenuMusic()
    {
        StopPlayMusic();
        musicsAudio.clip = musicClip[0];
        musicsAudio.loop = true;
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeClip); //дождаться окончания
        SelectLevelMusic();
        StartPlayMusic();
    }
}
