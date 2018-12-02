using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mace_Direct : MonoBehaviour
{
    public float projectileSpeed = 10f;

    public int damageAmount = 10;
    public string damageFunctionName = "DealDamage";

    public float timeToDie = 10f;

    private Rigidbody _rigidbody;
    private bool Reflected = false;
    public GameObject Father, Chain;

    public void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!Reflected)
        {
            this.transform.RotateAround(Father.transform.position, this.transform.right, 20 * Time.deltaTime);
        }
        else
            this.transform.Translate(this.transform.forward * projectileSpeed * Time.deltaTime, Space.World);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Deflector")
        {
            collision.gameObject.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
        else
        {
            Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            Reflected = true;
            Chain.SetActive(false);
            StartCoroutine(Timout());
            //Play sound
            GameObject.Find("UtilityManager").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        }
    }

    IEnumerator Timout()
    {
        yield return new WaitForSeconds(timeToDie);

        Destroy(this.gameObject);
    }

}