using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bomb_Direct : Projectile
{
    public float projectileSpeed = 10f;

    public int damageAmount = 10;
    public string damageFunctionName = "DealDamage";

    public float timeToDie = 5f;

    private Rigidbody _rigidbody;

    public GameObject projectilePrefab;
    //private int bombcount = 0;
    private bool reflected = false;
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
        if (!reflected)
        {
            if (collision.gameObject.name != "FrontBoxCollider" || collision.gameObject.name != "MidBoxCollider" || collision.gameObject.name != "BackBoxCollider")
            {
                Debug.Log("FAIL");
                collision.gameObject.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
                GameObject Explosion = (GameObject)Instantiate(projectilePrefab, this.transform.position, this.transform.rotation, null);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Bomb reflect");
                Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
                float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
                reflected = true;
                timeToDie = timeToDie + 2;
                //Play sound
                GameObject.Find("UtilityManager").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
            }
        }
    }

    IEnumerator Timout()
    {
        yield return new WaitForSeconds(timeToDie);
        GameObject Explosion = (GameObject)Instantiate(projectilePrefab, this.transform.position, this.transform.rotation, null);
        Destroy(this.gameObject);
    }

}
