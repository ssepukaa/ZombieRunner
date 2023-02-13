using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] arrayAudioClips;
    private AudioSource audioSource;
    private int randomItemAudioClip;


    public bool IsPlayeMusic { get; set; }
    private float timeClip;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        

       // audioSource.clip = arrayAudioClips[randomItemAudioClip];

    }
    void Update()
    {
        if (!IsPlayeMusic)    // если песня не играет
        {
            IsPlayeMusic = true;   //щелкнуть переключатель
            randomItemAudioClip = Random.Range(0, arrayAudioClips.Length);
            audioSource.clip = arrayAudioClips[randomItemAudioClip]; //выбрать случайную песню
            audioSource.Play(); //воспроизвести выбранную песню
            timeClip = arrayAudioClips[randomItemAudioClip].length * Time.timeScale;
            StartCoroutine(Wait()); //ждать ее окончания
        }
        if (Input.GetKeyDown(KeyCode.Tab))//переключить песню нажатием клавиши Tab
        {
            IsPlayeMusic = false; //щелкнуть переключатель назад
            audioSource.Stop(); //остановить песню
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeClip); //дождаться окончания
        IsPlayeMusic = false; //и щелкнуть переключатель назад
    }

   

    
}