
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Actor
{
    private GameObject player;
    private HealthEnemy healthEnemy;

    [SerializeField] private int damageAmount = 10;
    private bool isHit;

  

    void Start()
    {
        player = GameObject.Find("Player");
        healthEnemy = GetComponent<HealthEnemy>();
  

    }

    void Update()
    {

        if (player.transform.position.z > (transform.position.z + 20f))
        {
            DeactivateWithoutAnim();
          
        }

    }

    

    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.CompareTag("Player") /* && !isHit */)
        {

            Health health = other.GetComponent<Health>();
            health?.ApplyDamage(damageAmount);
            isHit = true;
           
            healthEnemy.Damage(100);
            
        }
    }
    private void DeactivateWithoutAnim()
    {
        healthEnemy.DieEnemy();
    }
    private void Deactivate()
    {
       
        healthEnemy.StartCoroutine("DieEffect");
        

       
    }

   

}
