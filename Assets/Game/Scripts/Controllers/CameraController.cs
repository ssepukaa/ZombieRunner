using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    //[SerializeField] private float offX =0f;
    //[SerializeField] private float offY =8.3f;
    [SerializeField] private float offZ =-6.5f;

   // private Vector3 offset;
    void Start()
    {
       
      // offset = transform.position - player.position;
    }

    
    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offZ + player.position.z);
        transform.position = newPosition;
    }
}
