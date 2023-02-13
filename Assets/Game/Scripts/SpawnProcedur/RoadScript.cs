
using UnityEngine;

public class RoadScript : MonoBehaviour
{

    private GameObject player;
    private GameObject roadController;
    private float _speedDirection;
    private RoadController _roadController;

    void Start()
    {
        player = GameObject.Find("Player");
        roadController = GameObject.Find("RoadGenerator");
        // RoadController _roadController = roadController.GetComponent<RoadController>();
        _roadController = roadController.GetComponent<RoadController>();


    }

   
    void Update()
    {
       // transform.Translate(-Vector3.forward * _speedDirection * Time.deltaTime);
        if (player.transform.position.z > (transform.position.z + 120f))
        {
            Deactivate();
            UpdateSpeedFromManager();
        }

    }


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void UpdateSpeedFromManager()
    {
        _speedDirection = _roadController.SpeedDirection;
    }
}
