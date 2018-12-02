using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSounds : MonoBehaviour {

    public GameObject soundSource;

    private AudioSource[] soundBank;

    // Use this for initialization
    void Start ()
    {
         soundBank = soundSource.GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag== "Deflector")
        {
            soundBank = soundSource.transform.GetChild(0).GetComponents<AudioSource>();
        }
        else if(collision.collider.tag == "Player" || collision.collider.tag == "Enemy")
        {
            soundBank = soundSource.transform.GetChild(1).GetComponents<AudioSource>();
        }
        else
        {
            soundBank = soundSource.transform.GetChild(2).GetComponents<AudioSource>();
        }

        soundBank[Random.Range(0, soundBank.Length)].Play();
    }
}
