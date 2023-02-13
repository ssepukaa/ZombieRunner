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
        if (!IsPlayeMusic)    // ���� ����� �� ������
        {
            IsPlayeMusic = true;   //�������� �������������
            randomItemAudioClip = Random.Range(0, arrayAudioClips.Length);
            audioSource.clip = arrayAudioClips[randomItemAudioClip]; //������� ��������� �����
            audioSource.Play(); //������������� ��������� �����
            timeClip = arrayAudioClips[randomItemAudioClip].length * Time.timeScale;
            StartCoroutine(Wait()); //����� �� ���������
        }
        if (Input.GetKeyDown(KeyCode.Tab))//����������� ����� �������� ������� Tab
        {
            IsPlayeMusic = false; //�������� ������������� �����
            audioSource.Stop(); //���������� �����
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeClip); //��������� ���������
        IsPlayeMusic = false; //� �������� ������������� �����
    }

   

    
}