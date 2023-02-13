using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] Text recordText;
    private GameObject soundObject;
    //private VolumeManager volumeManager;
    //[SerializeField] private PlayerControll playerControll;

    public void Start()
    {
        soundObject = GameObject.FindGameObjectWithTag("SoundManager");
        //volumeManager =soundObject.GetComponent<VolumeManager>();
        //playerControll = GetComponent<PlayerControll>();

        int lastDistaneScore = PlayerPrefs.GetInt("lastDistanceScore");
        int recordDistanceScore = PlayerPrefs.GetInt("recordDistanceScore");
        int coinsRec = PlayerPrefs.GetInt("coinsRec");
        int coinsCurrent = PlayerPrefs.GetInt("coinsCurrent");
        if(lastDistaneScore > recordDistanceScore)
        {
            recordDistanceScore = lastDistaneScore;
            PlayerPrefs.SetInt("recordDistanceScore", recordDistanceScore);
            recordText.text = recordDistanceScore.ToString();
        }
        else
        {
            recordText.text = recordDistanceScore.ToString();
        }

        if (coinsCurrent>coinsRec)
        {
            coinsRec = coinsCurrent;
            PlayerPrefs.SetInt("coinsRec", coinsCurrent);
            PlayerPrefs.SetInt("coinsCurrent", 0);
        }
        else
        {
            PlayerPrefs.SetInt("coinsCurrent", 0);
        }

    }
    //private void Awake()
    //{
    //    volumeManager?.StopPlayMusic();
    //    volumeManager?.SelectLosePanelMusic();
    //    volumeManager?.StartPlayMusic();
        
    //}
    public void Restart()
    {
        SceneManager.LoadScene(1);
        //playerControll.LiveAgain();
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale=1;
    }
}
