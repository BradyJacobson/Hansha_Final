using System;
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

    public int _currentHealth;
    private bool _canTakeDamage = true;

    public float DeathAnimTime;

    private FollowGuyEnemyAnimationScript followGuyAnimScript;
    private StandEnemyAnimationScript standGuyAnimScript;
    private Weapon_Projectile weaponProjectileScript;

    public void Start()
    {
        _currentHealth = startingHealth;
        if (this.tag == "FollowEnemy")
        {
            followGuyAnimScript = GetComponent<FollowGuyEnemyAnimationScript>();
        }
        else if(this.tag == "StandEnemy")
        {
            standGuyAnimScript = GetComponent<StandEnemyAnimationScript>();
        }
        else if(this.tag == "Enemy_Turret")
        {
            weaponProjectileScript = GetComponent<Weapon_Projectile>();
        }
    }

    void Update()
    {
        if (this.tag == "FollowEnemy")
        {
            followGuyAnimScript.currentHealth = _currentHealth;
        }
        else if (this.tag == "StandEnemy")
        {
            standGuyAnimScript.currentHealth = _currentHealth;
        }
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
            if (this.tag == "Enemy_Turret")
            {
                _currentHealth = 0;
                deathEvent.Invoke();
            }
            else
            {
                PlayerDeath();
                _currentHealth = 0;
            }
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

