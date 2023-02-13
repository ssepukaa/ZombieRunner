using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerManagerObstacle1 : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjectLayer;
    [SerializeField] private Transform player;
    [SerializeField] private float[] positionX = { -1.5f, 0, 1.5f };
    [SerializeField] private int count = 20;

    //private bool isCanSpawn = false;
    private float maxOffsetZ = 100f;
    private float speed = 10f;
    private int tempCount;

    
    

    void Start()
    {
         
        tempCount = count;
        MoveSpawner(speed);


    }
   

    private void Update()
    {

        if (transform.position.z - player.position.z < maxOffsetZ && tempCount!=count)
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
            new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
        Collider[] collidersInsideOverlapBox = new Collider[1];
        int numberOfCollidersFound =
            Physics.OverlapBoxNonAlloc(transform.position, overlapTestBoxScale, collidersInsideOverlapBox, transform.rotation, spawnedObjectLayer);

        if (numberOfCollidersFound == 0)
        {
           
            Spawn();

        }
        else
        {
            Debug.Log("name of collider 0 found " + collidersInsideOverlapBox[0].name);
            ShiftSpawner(1f);

        }
    }

    
    private void Spawn()
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);

        GameObject clone = Instantiate(itemsToPickFrom[randomIndex],
        new Vector3(transform.position.x, transform.position.y - 1f,
        transform.position.z), transform.rotation);






        CountSpawner();
      
    }

    void ShiftSpawner(float shiftSpeed)
    {
        MoveSpawner(shiftSpeed);

    }

    void CountSpawner()
    {

        tempCount = count;
        count--;
       
    }


}
