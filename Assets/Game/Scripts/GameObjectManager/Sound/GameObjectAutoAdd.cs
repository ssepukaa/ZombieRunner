using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAutoAdd : MonoBehaviour
{
    public string nameObject;
    
    void Start()
    {
        //Add this GameObject to List when it is created
        GameObjectManager.instance.allObjects.Add(nameObject, gameObject);
    }
   

    void OnDestroy()
    {
        //Remove this GameObject from the List when it is about to be destroyed
        GameObjectManager.instance.allObjects.Remove(nameObject);
    }
}