using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOIMusicChangeModeAdd : MonoBehaviour
{
    public string nameObjectForDictionary;
    
    void Start()
    {
        //Add this GameObject to List when it is created
        GameObjectManager.instance.DicILevelMusicChangeModeObjects.Add(nameObjectForDictionary, gameObject);
    }
   

    void OnDestroy()
    {
        //Remove this GameObject from the List when it is about to be destroyed
        GameObjectManager.instance.DicILevelMusicChangeModeObjects.Remove(nameObjectForDictionary);
    }
}