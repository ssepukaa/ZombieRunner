using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPref : MonoBehaviour
{
    private Rigidbody playerRb;

    private Vector3 dir;
    [SerializeField] private int speed;





    void Start()
    {

        playerRb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
     //   MovePosition();
        
    }
    void MovePosition()
    {

        dir.z = speed;
        playerRb.MovePosition(transform.position - new Vector3(0, 0, speed * Time.deltaTime));
        
        if (transform.position.z <= -10.0f)
        {
            playerRb.transform.position = new Vector3(1.5f, 0.99f, 50.0f);


        }
    }

}
