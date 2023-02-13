using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayComponet : ActorComponent
{
    private GameObject _soundManager;
    private VolumeManager _volumeManager;
    //[SerializeField] private int numberShootSoundClip;

    private void Start()
    {
        _soundManager = GameObjectManager.instance.allObjects["SoundManager"];
        _volumeManager = _soundManager.GetComponent<VolumeManager>();
    }   

    public void  PlaySound(int numberShootSoundClip)
    {
        _volumeManager?.PlaySFX(numberShootSoundClip);
    }
}
