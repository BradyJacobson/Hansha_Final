using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHP : MonoBehaviour
{

    public int startingHealth = 30;

    public UnityEvent damageEvent;
    public UnityEvent deathEvent;
    public GameObject Door;

    private int _currentHealth;
    private bool _canTakeDamage = true;

    public float DeathAnimTime;

    public void Start()
    {
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
            if(Door)
            {
                Door.GetComponent<Locked_Door>().LoseCharacter();
            }
            PlayerDeath();
            _currentHealth = 0;
        }
    }

    public void PlayerDeath()
    {
        deathEvent.Invoke();
        _canTakeDamage = false;
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(DeathAnimTime);
        Destroy(this.gameObject);
    }
}

