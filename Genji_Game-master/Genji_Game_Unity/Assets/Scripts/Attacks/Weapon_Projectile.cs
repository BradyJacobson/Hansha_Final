using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Projectile : Weapon {

    public GameObject projectilePrefab;

    public float fireRate = 0.25f;

    public bool _canFire = true;

    private EnemyHP enemyHPScript;

    void Start()
    {
        enemyHPScript = GetComponent<EnemyHP>(); 
    }

    public override void Fire (Transform attackSpawnPoint)
    {
        if (_canFire)
        {

            GameObject projectile = (GameObject)Instantiate(projectilePrefab, attackSpawnPoint.position, attackSpawnPoint.rotation, null);

            _canFire = false;

            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown ()
    {
        yield return new WaitForSeconds(fireRate);

        if (enemyHPScript._currentHealth > 0)
        {
            _canFire = true;
        }
    }
}
