using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : ActorComponent
{
    private int currentHealth;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private ParticleSystem _bloodSprayEffect;
    [SerializeField] private ParticleSystem _bloodStreamEffect;
    private Animator anim;
    private Collider colliderSelf;
    
    private bool isActive = true;

    

    private EnemyBase enemyBase;

    private GameObject _soundManager;
    private VolumeManager _volumeManager;
    [SerializeField] private int numberDeathSoundClip;

    private void Start()
    {
        anim = GetComponent<Animator>();

        colliderSelf = GetComponent<Collider>();

        enemyBase = GetComponent<EnemyBase>();

        _soundManager = GameObjectManager.instance.allObjects["SoundManager"];
        _volumeManager = _soundManager.GetComponent<VolumeManager>();

        currentHealth = maxHealth;
        colliderSelf.enabled = true;

       
    }

    private void OnEnable()
    {
        if (!isActive)
        {
            currentHealth = maxHealth;
            colliderSelf.enabled = true;
            isActive = true;
           

        }

    }
    public void Damage(int damageAmount, RaycastHit hit)
    {
        ParticleSystem bloodSprayEffect = _bloodSprayEffect;
        ParticleSystem bloodStreamEffect = _bloodStreamEffect;
        if (currentHealth <= damageAmount)
        {
            if (bloodStreamEffect)
            {
                bloodStreamEffect = Instantiate(bloodStreamEffect, hit.point, Quaternion.LookRotation(hit.normal));

            }
        }

        if (bloodSprayEffect)
        {
            bloodSprayEffect = Instantiate(bloodSprayEffect, hit.point, Quaternion.LookRotation(hit.normal));

        }
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {


            colliderSelf.enabled = false;


            if (bloodStreamEffect)
            {
               // Destroy(bloodStreamEffect.gameObject, 2f);
            }


            DieEffect();


        }
        if (bloodSprayEffect)
        {
           // Destroy(bloodSprayEffect.gameObject, 2f);
        }

    }
    public void Damage(int damageAmount)
    {
        ParticleSystem bloodSprayEffect = _bloodSprayEffect;
        ParticleSystem bloodStreamEffect = _bloodStreamEffect;
        if (currentHealth <= damageAmount)
        {
            if (bloodStreamEffect)
            {
                bloodStreamEffect = Instantiate(bloodStreamEffect, transform.position, Quaternion.LookRotation(transform.position));

            }
        }

        if (bloodSprayEffect)
        {
            bloodSprayEffect = Instantiate(bloodSprayEffect, transform.position, Quaternion.LookRotation(transform.position));

        }
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {


            colliderSelf.enabled = false;


            if (bloodStreamEffect)
            {
             //   Destroy(bloodStreamEffect.gameObject, 2f);
            }
            if (isActive)
            {
            
                DieEffect();

            }

        }
        if (bloodSprayEffect)
        {
         //   Destroy(bloodSprayEffect.gameObject, 2f);
        }

    }
    public void DieEffect()
    {
        // enemyBase.PlayAudio();
        _volumeManager.PlaySFX(numberDeathSoundClip);

        if (anim)
        {
            anim.SetTrigger("isDie");
            StartCoroutine("WaitAnim");

        }
        else
        {
            BarrelScript barrelScript = GetComponent<BarrelScript>();
            barrelScript?.ExplosiveBarrel();
            DieEnemy();
        }



    }
    public IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(2f);
        DieEnemy();
    }
    public void DieEnemy()
    {

       

        gameObject.SetActive(false);
        isActive = false;
        

    }



}
