using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipSource : MonoBehaviour
{
    [SerializeField] private AudioClip clip;    
    void Start()
    {
        clip = GetComponent<AudioClip>();
    }

    
    
}
