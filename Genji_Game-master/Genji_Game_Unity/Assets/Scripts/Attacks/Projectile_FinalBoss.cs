﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile_FinalBoss : Projectile
{
    public float projectileSpeed = 10f;

    public int damageAmount = 10;
    public string damageFunctionName = "DealDamage";

    public float timeToDie = 1.5f;

    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();

        StartCoroutine(Timout());
    }

    void Update()
    {
        this.transform.Translate(this.transform.forward * projectileSpeed * Time.deltaTime, Space.World);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Deflect")
        {
            this.transform.forward = -this.transform.forward;

            //Play sound
            GameObject.Find("Deflect_Sounds").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        }
        else if (collision.gameObject.tag != "Projectile")
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
            {
                GameObject.Find("Damage_Sounds").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
            }
            else
            {
                GameObject.Find("Impact_Sounds").GetComponent<AudioSource>().Play();
            }
            collision.gameObject.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Timout()
    {
        yield return new WaitForSeconds(timeToDie);

        Destroy(this.gameObject);
    }

}
