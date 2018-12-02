using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile_Direct : Projectile
{
    public float projectileSpeed = 10f;

    public int damageAmount = 10;
    public string damageFunctionName = "DealDamage";

    public float timeToDie = 5f;

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
            Debug.Log("A");
            Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);

            //Play sound
            GameObject.Find("Deflect_Sounds").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        }
        else if (collision.gameObject.tag != "Projectile")
        {
            Debug.Log("b");
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
            {
                Debug.Log("OUCHIE");
              //  GameObject.Find("Damage_Sounds").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
            }
            else
            {
           //     GameObject.Find("Impact_Sounds").GetComponent<AudioSource>().Play();
            }
            collision.gameObject.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
            Debug.Log("SHOULD BE DED");
        }



        //else if (collision.gameObject.name == "MidBoxCollider")
        //{
        //    Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        //    float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg + Random.Range(1, 16);
        //    transform.eulerAngles = new Vector3(0, rot, 0);

        //    //Play sound
        //    GameObject.Find("UtilityManager").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        //}
        //else if (collision.gameObject.name == "BackBoxCollider")
        //{
        //    Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        //    float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg - Random.Range(16,31);
        //    transform.eulerAngles = new Vector3(0, rot, 0);

        //    //Play sound
        //    GameObject.Find("UtilityManager").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        //}
        //else
        //{
        //    // print(GameObject.Find("Deflector").GetComponents<BoxCollider>().Length);

        //    collision.gameObject.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);

        //    Destroy(this.gameObject);
        //}
    }

    IEnumerator Timout()
    {
        yield return new WaitForSeconds(timeToDie);

        Destroy(this.gameObject);
    }

}
