using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : ActorComponent
{


    private int gunDamage = 1;
    [SerializeField] private float fireRate = 0.00f; // 0.07
    [SerializeField] private float weaponRange = 25f;
    [SerializeField] private float shotDuration = 0.07f;
    [SerializeField] private float nextFireTime = 0.00f; // время, через которое можно будет снова стрелять 0.07

    [SerializeField] private Transform gunEnd;

    [SerializeField] private ParticleSystem muzzleFlash;
    

    
    private LineRenderer shotLine;
    private PlayerControll playerController;

    [SerializeField] LayerMask layerMaskEnemy;

    // взрыв бочки ExplosiveScript

    [SerializeField] private float overlapTestBoxSizeX = 8f;
    [SerializeField] private float overlapTestBoxSizeY = 2f;
    [SerializeField] private float overlapTestBoxSizeZ = 8f;
    public LayerMask spawnedObjectLayer;
    private Collider[] enemyColliders = new Collider[50];
    [SerializeField] private int damageExplosive = 3;

    Vector3 transGizmo;

    /// звук

    private GameObject _soundManager;
    private VolumeManager _volumeManager;
    [SerializeField] private int numberShootSoundClip;
    


    private void Start()
    {

       
        shotLine = GetComponent<LineRenderer>();
        playerController = GetComponent<PlayerControll>();

        
        _soundManager = GameObjectManager.instance.allObjects["SoundManager"];
        _volumeManager = _soundManager.GetComponent<VolumeManager>();



        shotLine.startWidth = 0.2f;
        shotLine.endWidth = 0.02f;
        shotLine.positionCount = 2;



    }

    
    public void TryFireWeapon()
    {
        Fire();
    }


    private void Fire()
    {




        if (Time.time > nextFireTime && playerController.onGround)
        {

            nextFireTime = Time.time + fireRate;

            Vector3 rayOrigin = gunEnd.position;

            Vector3 rayForward = gunEnd.position + new Vector3(0, 0, weaponRange);
            RaycastHit hit;
            shotLine.SetPosition(0, gunEnd.position);

            StartCoroutine("EShotEffect");



            if (Physics.Raycast(gunEnd.transform.position, transform.forward, out hit, weaponRange, layerMaskEnemy))
            {
                shotLine.SetPosition(1, hit.point);

                FireWeapon(hit);

            }
            else
            {
                shotLine.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z + weaponRange));

            }
        }
    }




    private void FireWeapon(RaycastHit hit)
    {

        HealthEnemy healthEnemy = hit.collider.GetComponent<HealthEnemy>();
        if (healthEnemy != null)
        {
            
            healthEnemy.Damage(gunDamage, hit);
        
        }

    }

    

    public IEnumerator EShotEffect()
    {
        if (muzzleFlash)
        {
            muzzleFlash.Play();
        }



        _volumeManager?.PlaySFX(numberShootSoundClip);

        shotLine.enabled = true;

        yield return new WaitForSeconds(shotDuration);

        shotLine.enabled=false;
    }


    public void Explosive(Transform transform)
    {
        enemyColliders= PositionRaycastExplosive(transform);
   

        foreach (var item in enemyColliders)
        {
            if (item != null)
            {
              
                HealthEnemy healthEnemy = item.GetComponent<HealthEnemy>();
                if (healthEnemy != null)
                {

                    healthEnemy.Damage(damageExplosive);
               
                }
            }
        }


    }
    private Collider[] PositionRaycastExplosive(Transform transform)
    {
        transGizmo=transform.position + new Vector3(0,1.2f,0);
        Vector3 overlapTestBoxScale =
            new Vector3(overlapTestBoxSizeX, overlapTestBoxSizeY, overlapTestBoxSizeZ);
        Collider[] collidersInsideOverlapBox = new Collider[50];
        
        int numberOfCollidersFound =
            Physics.OverlapBoxNonAlloc(transGizmo, overlapTestBoxScale, collidersInsideOverlapBox, transform.rotation, spawnedObjectLayer);
   
        return collidersInsideOverlapBox;
       
    }
    //void OnDrawGizmos()
    //{
    //    // Draw a semitransparent blue cube at the transforms position
    //    Gizmos.color = new Color(1, 0, 0, 0.5f);
    //    Gizmos.DrawCube(transGizmo, new Vector3(overlapTestBoxSizeX, overlapTestBoxSizeY, overlapTestBoxSizeZ));
    //}

}
