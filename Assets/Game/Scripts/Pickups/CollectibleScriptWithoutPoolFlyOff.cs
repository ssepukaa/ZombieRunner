using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;


//[RequireComponent(typeof(AudioSource))]
public class CollectibleScriptWithoutPoolFlyOff : MonoBehaviour
{

    public enum CollectibleTypes { NoType, Type1, Type2, Type3, Type4, Type5 }; // you can replace this with your own labels for the types of collectibles in your game!

    public CollectibleTypes CollectibleType; // this gameObject's type

    public bool rotate; // do you want it to rotate?

    public float rotationSpeed = 60f;
    [SerializeField] private float moveSpeed = 1f;


    //public AudioClip collectSound;

    public GameObject collectEffect;
    private bool isUpMove = true;
    private bool isDownMove = false;
    bool isActive = true;

    [SerializeField] private float upRangeMove = 1.5f;
    [SerializeField] private float downRangeMove = 0.5f;

    //Полет монеты к очкам


    private AudioPlayComponet audioPlayComponent;
    [SerializeField] private int numberShootSoundClip;



    // Use this for initialization
    void Start()
    {
       
        audioPlayComponent = gameObject.AddComponent<AudioPlayComponet>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rotate)
        {

            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }

        MovingUpDown();

    }

   
    void MovingUpDown()
    {
        if (isUpMove)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

            if (transform.position.y > upRangeMove)
            {
                transform.position =new Vector3(transform.position.x, upRangeMove, transform.position.z);

                isUpMove = false;
                isDownMove = true;

            }
        }
        if (isDownMove)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * moveSpeed);

            if (transform.position.y < downRangeMove)
            {
                transform.position =new Vector3(transform.position.x, downRangeMove, transform.position.z);

                isUpMove = true;
                isDownMove = false;

            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.CompareTag("cleaner"))
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);

        }
        if (other.tag == "Player")
        {
           // isActive = false;
            Collect();
        }

    }

    public void Collect()
    {

        audioPlayComponent?.PlaySound(numberShootSoundClip);

        if (collectEffect)
        {
            var effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, 2f);
        }

        //Below is space to add in your code for what happens based on the collectible type

        if (CollectibleType == CollectibleTypes.NoType)
        {

            //Add in code here;

            Debug.Log("Do NoType Command");
        }
        if (CollectibleType == CollectibleTypes.Type1)
        {

            //Add in code here;

            Debug.Log("Do NoType Command");
        }
        if (CollectibleType == CollectibleTypes.Type2)
        {

            //Add in code here;

            Debug.Log("Do NoType Command");
        }
        if (CollectibleType == CollectibleTypes.Type3)
        {

            //Add in code here;

            Debug.Log("Do NoType Command");
        }
        if (CollectibleType == CollectibleTypes.Type4)
        {

            //Add in code here;

            Debug.Log("Do NoType Command");
        }
        if (CollectibleType == CollectibleTypes.Type5)
        {

            //Add in code here;

            Debug.Log("Do NoType Command");
        }

        StartCoroutine("Delay");

    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
        //gameObject.SetActive(false);
        //isActive=true;
    }
}
