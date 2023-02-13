using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    private GameObject player;
    private WeaponSystem weaponSystem;
    [SerializeField] private bool canExplosive = false;
    
    void Start()
    {
        player = GameObject.Find("Player");
        weaponSystem=player.GetComponent<WeaponSystem>();
       
    }

    public void ExplosiveBarrel()
    {
        if (canExplosive)
        {
            weaponSystem.Explosive(transform);

        }
    }





}
