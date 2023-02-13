using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour, IDamageable
{
    [Header("HealthStats")]
    [SerializeField] private int _maxHealth = 100;
    private PlayerControll playerController;
    private int _currentHealth;
    public event Action<float> HealthChanged;

    //private AudioSource takeDamageSound;
    //[SerializeField] private ParticleSystem bloodSprayEffect;

    private void Start()
    {
        playerController = GetComponent<PlayerControll>();
        _currentHealth=_maxHealth;
       // takeDamageSound = GetComponent<AudioSource>();


    }
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            ApplyDamage(UnityEngine.Random.Range(2, 10));
           

        }
    }

    public void ApplyDamage(int value)
    {
        if (HealthChanged != null)
        {
            //takeDamageSound.Play();
            //Instantiate(bloodSprayEffect, transform.position, Quaternion.LookRotation(transform.position));

            if (value >= 0)
            {
                _currentHealth-=value;
                if (_currentHealth<=0)
                {
                    Death();
                }
                else
                {
                    float _currentHealthAsPercentage = (float)_currentHealth/_maxHealth;
                    HealthChanged?.Invoke(_currentHealthAsPercentage);
                }
            }
        }
    }
    private void ChangeHealth(int value)
    {
        if (value>=0)
        {

            _currentHealth+=value;
            if (_currentHealth<=0)
            {


                Death();
            }
            else
            {
                float _currentHealthAsPercentage = (float)_currentHealth/_maxHealth;
                HealthChanged?.Invoke(_currentHealthAsPercentage);
            }
        }
        else
        {
            Debug.Log("Отрицательное значение урона!");
        }

    }
    private void Death()
    {
        HealthChanged?.Invoke(0);

        playerController.Dead();

        
    }
}
