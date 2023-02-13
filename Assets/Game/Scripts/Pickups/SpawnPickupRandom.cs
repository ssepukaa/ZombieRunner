using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickupRandom : MonoBehaviour
{
    [SerializeField] private Dictionary<string,GameObject> spawnPickups = new Dictionary<string, GameObject>();


    private void OnDestroy()
    {


        //if (spawnPickups != null && )
        //{
        //    Instantiate(spawnPickups[ Random.Range(0, spawnPickups.Length)], transform.position, Quaternion.identity);
        //}
    }
}
