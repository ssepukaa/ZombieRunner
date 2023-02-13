using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour    
{
    [SerializeField] private Transform player;
    [SerializeField] private Text distanceText;
    private int distanceCount = 0;
   
    

    private void Start()
    {



    }
    private void Update()
    {
        distanceText.text =  ((int)(player.position.z/2.5f)) .ToString();
    }
   
    private void SetDistanceCountText()
    {

        distanceText.text = distanceCount.ToString();
    }
    public int GetDistanceCount()
    {
        return distanceCount;
    }


}
