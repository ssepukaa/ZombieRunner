using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    
    private void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (!CanPlay) return;

        
        if (other.gameObject.CompareTag("cleaner"))
        {
           
            Destroy(gameObject);
            
          
        }




    }



}

