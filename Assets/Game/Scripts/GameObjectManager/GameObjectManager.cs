using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    //Holds instance of GameObjectManager
    public static GameObjectManager instance = null;
    public Dictionary<string,GameObject> allObjects = new Dictionary<string, GameObject>();
    public Dictionary<string,GameObject> DicILevelMusicChangeModeObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }
    private void Update()
    {
       
    
     
    }
}