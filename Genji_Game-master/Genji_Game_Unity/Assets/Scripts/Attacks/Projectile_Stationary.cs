using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Stationary : MonoBehaviour
{
    public float projectileSpeed = 10f;

    public int damageAmount = 10;
    public string damageFunctionName = "DealDamage";

    private float timeToDie = 5f;

    private Rigidbody _rigidbody;

    private Vector3 Origin;
    private bool Activated;

    public void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        Activated = false;
        Origin = this.transform.position;
    }

    void Update()
    {
        if (Activated)
            this.transform.Translate(this.transform.forward * projectileSpeed * Time.deltaTime, Space.World);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "FrontBoxCollider")
        {
            this.transform.forward = -collision.gameObject.transform.forward;
            Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            Activated = true;
            StartCoroutine(Timout());
            //Play sound
            GameObject.Find("UtilityManager").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        }

        //else if (collision.gameObject.name == "MidBoxCollider")
        //{
        //    Debug.Log("M OK");
        //    this.transform.forward = -collision.gameObject.transform.forward;
        //    Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        //    float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg + Random.Range(60, 121);
        //    transform.eulerAngles = new Vector3(0, rot, 0);
        //    Activated = true;
        //    StartCoroutine(Timout());
        //    //Play sound
        //    GameObject.Find("UtilityManager").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        //}

        //else if (collision.gameObject.name == "BackBoxCollider")
        //{
        //    Debug.Log("B OK");
        //    this.transform.forward = -collision.gameObject.transform.forward;
        //    Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        //    float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg - Random.Range(45, 136); ;
        //    transform.eulerAngles = new Vector3(0, rot, 0);
        //    Activated = true;
        //    StartCoroutine(Timout());
        //    //Play sound
        //    GameObject.Find("UtilityManager").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        //}

        else
        {
            collision.gameObject.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
            this.transform.position = Origin;
            Activated = false;
        }
    }

    IEnumerator Timout()
    {
        yield return new WaitForSeconds(timeToDie);

        this.transform.position = Origin;
    }

}
