using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHP : MonoBehaviour
{


    public int startingHealth = 10;

    private float minus = -0.67f;
    public UnityEvent damageEvent;
    public UnityEvent deathEvent;

    public GameObject health1, health2, health3;

    public int _currentHealth;
    private bool _canTakeDamage = true;

    [Header("UI Properties")]
    public GameObject Health;

    public void Start()
    {
        health1.SetActive(true);
        health2.SetActive(false);
        health3.SetActive(false);
        _currentHealth = startingHealth;
    }

    public void DealDamage(int damage)
    {
        if (!_canTakeDamage)
            return;

        _currentHealth -= damage;

        damageEvent.Invoke();

        if (_currentHealth <= 0)
        {
            Destroy(Health);
            PlayerDeath();
            _currentHealth = 0;
        }
        else
        {
            Health.transform.localScale += new Vector3(minus, 0, 0);
            if(_currentHealth <= 20)
            {
                health1.SetActive(false);
                health2.SetActive(false);
                health3.SetActive(true);
            }
            else if (_currentHealth <= 40)
            {
                health1.SetActive(false);
                health2.SetActive(true);
                health3.SetActive(false);
            }
            else
            {
                health1.SetActive(true);
                health2.SetActive(false);
                health3.SetActive(false);
            }
        }
    }

    public void PlayerDeath()
    {
        deathEvent.Invoke();
        _canTakeDamage = false;
        Destroy(this.gameObject);
    }
}

