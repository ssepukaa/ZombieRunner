using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private List<GameObject> activeRoads = new List<GameObject>();
    public GameObject[] tilePrefabs;
    private float spawnPosZ = 80f;
    private float tileLenght = 200f;
    private int tiles = 2;

    [SerializeField] private float speedDirection = 0f;
    // Пулинг
    public GameObject roadPref;
    //List<GameObject> roads;

    public float SpeedDirection
    {
        get { return speedDirection; }
        set { speedDirection=SpeedDirection; }
    }
    void Start()
    {
        for (int i = 0; i < tiles; i++)
        {
            SpawnTile(0);

        }
    }

   void Update()
    {
        if (player.position.z - 120f > (spawnPosZ - tiles * tileLenght))
        {
            ActivateTile();
            
            
        }
        
       
    }

    private GameObject SpawnTile(int roadIndex)
    {
        GameObject road = Instantiate(tilePrefabs[roadIndex], transform.forward * spawnPosZ, transform.rotation);
        activeRoads.Add(road);
        road.gameObject.SetActive(false);
        return road;

    }
    private void DeleteTile()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
        
    }
    private void ActivateTile()
    {
        GameObject newRoad = GetFreeRoad();
        newRoad.transform.position = transform.forward * spawnPosZ;
        newRoad.SetActive(true);
        spawnPosZ += tileLenght;
        

    }
    private void DisactivateTile()
    {
       
    }
    public GameObject GetFreeRoad()
    {
        for(int i = 0; i <activeRoads.Count; i++)
        {
            if (!activeRoads[i].activeInHierarchy)
            {
                return activeRoads[i];
            }
        }

       return SpawnTile(0);
       
       
        
    }
}
