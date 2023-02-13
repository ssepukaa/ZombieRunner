using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private Health _health;
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(float valueAsPercantage)
    {
        //Debug.Log(valueAsPercantage);
        _healthBarFilling.fillAmount = valueAsPercantage;
    }
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }

}
