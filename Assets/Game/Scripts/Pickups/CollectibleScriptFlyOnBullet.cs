using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;


//[RequireComponent(typeof(AudioSource))]
public class CollectibleScriptFlyOnBullet : MonoBehaviour
{

    public enum CollectibleTypes { NoType, Type1, Type2, Type3, Type4, Type5 }; // you can replace this with your own labels for the types of collectibles in your game!

    public CollectibleTypes CollectibleType; // this gameObject's type

    public bool rotate; // do you want it to rotate?

    public float rotationSpeed = 60f;
    [Tooltip("Скорость подпрыгивания в режиме ожидания")]
    [SerializeField] private float moveSpeed = 1f;


    //public AudioClip collectSound;

    public GameObject collectEffect;
    private bool isUpMove = true;
    private bool isDownMove = false;
    bool isActive = true;

    [SerializeField] private float upRangeMove = 1.5f;
    [SerializeField] private float downRangeMove = 0.5f;

    //Полет монеты к очкам
    [Tooltip("Скорость перемещения к сборщику очков")]
    [SerializeField] private float speed = 0.4f;
    private GameObject collectorCoinsFictive;
    [SerializeField] private float minimum = 0.1f;
    [SerializeField] private float maximum = 1.0F;
    [Tooltip("Скорость уменьшения")]
    [SerializeField] private float speedScale = 0.03f;

    private AudioPlayComponet audioPlayComponent;
    [SerializeField] private int numberShootSoundClip = 2;

    // Use this for initialization
    void Start()
    {
        collectorCoinsFictive = GameObject.Find("CollectorCoinsFictiveAmmo");
        audioPlayComponent = gameObject.AddComponent<AudioPlayComponet>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rotate)
        {

            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }



    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            MovingUpDown();
        }

        if (!isActive)
        {
            MoveByCollect();
        }

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
    void MoveByCollect()
    {



        transform.position = Vector3.MoveTowards(transform.position, collectorCoinsFictive.transform.position, speed);
        if(transform.localScale.x >minimum)
        {
            transform.localScale -= new Vector3(speedScale, speedScale, speedScale);
        }
        



    }
    void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.CompareTag("cleaner"))
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);

        }
        if (other.tag == "Player")
        {
            isActive = false;
            Collect();
        }

    }

    public void Collect()
    {
       
            //  AudioSource.PlayClipAtPoint(collectSound, transform.position);
            audioPlayComponent?.PlaySound(numberShootSoundClip);

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
        yield return new WaitForSeconds(1f);
        if (collectEffect)
        {
            var effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            effect.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Destroy(effect.gameObject, 2f);
        }
        gameObject.SetActive(false);
        isActive=true;
        transform.localScale = new Vector3(maximum, maximum, maximum);
    }
}
