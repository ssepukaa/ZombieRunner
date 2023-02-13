using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpawnerManagerEnemy : MonoBehaviour
{
    public GameObject[] enemyPref;
    [SerializeField] private float overlapTestBoxSizeX = 1f;
    [SerializeField] private float overlapTestBoxSizeY = 0.5f;
    [SerializeField] private float overlapTestBoxSizeZ = 1f;
    public LayerMask spawnedObjectLayer;
    [SerializeField] private Transform player;
    [SerializeField] private float[] positionX = { -1.5f, 0, 1.5f };
    //[SerializeField] private int count = 20;
    private int debugCount;

    //private bool isCanSpawn = false;
    private float maxOffsetZ = 100f;
    [SerializeField] private float speed = 2f;
    private int tempCount;

    //Пулинг
    private List<GameObject> activePrefs = new List<GameObject>();
    
    


    // DEBUGER

    //public Text debugCountText;
    //public Text debugCountListText;


    void Start()
    {
        
       
        MoveSpawner(speed);

        
    }
   

    private void Update()
    {

        if (transform.position.z - player.position.z < maxOffsetZ /* && tempCount!=count*/)
        {
            MoveSpawner(speed);
        }
       
    }
    void MoveSpawner(float speed)
    {
        Vector3 newPosition =
            new Vector3(positionX[Random.Range(0, positionX.Length)], transform.position.y, transform.position.z + speed);
        transform.position = newPosition;
        PositionRaycast();

    }

    void PositionRaycast()
    {

        Vector3 overlapTestBoxScale =
            new Vector3(overlapTestBoxSizeX, overlapTestBoxSizeY, overlapTestBoxSizeZ);
        Collider[] collidersInsideOverlapBox = new Collider[1];
        int numberOfCollidersFound =
            Physics.OverlapBoxNonAlloc(transform.position, overlapTestBoxScale, collidersInsideOverlapBox, transform.rotation, spawnedObjectLayer);

        if (numberOfCollidersFound == 0)
        {
           
            Spawn();

        }
        else
        {
            
            ShiftSpawner(1f);

        }
    }

    
    private bool Spawn()
    {
        bool hasGetFree = false;
        for (int i=0; i<activePrefs.Count;i++)
        {
            if (!activePrefs[i].activeInHierarchy)
            {
                GameObject newClone = activePrefs[i].gameObject;
                newClone.transform.position = transform.position + new Vector3(0,-1f,0);
                newClone.SetActive(true);

                hasGetFree = true;
               
                return hasGetFree;
               
                
            }
            
        }
        
        if (!hasGetFree)
        {
            int randomIndex = Random.Range(0, enemyPref.Length);
            GameObject clone = Instantiate(enemyPref[randomIndex],
            new Vector3(transform.position.x, transform.position.y-1f,
            transform.position.z), transform.rotation);
            clone.SetActive(true);
            activePrefs.Add(clone);
  
        }
        hasGetFree = false;
        return hasGetFree;



    }

    void ShiftSpawner(float shiftSpeed)
    {
        MoveSpawner(shiftSpeed);

    }

    
   
}
