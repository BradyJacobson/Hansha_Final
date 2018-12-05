using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointInteraction : MonoBehaviour
{
    public GameObject Player;
    public int Order;
    public AudioSource activationSound;

    private bool activated;

    void Start()
    {
        activated = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && !activated)
        {
            PlayerPrefs.SetInt("checkpoint", Order);
            activated = true;
            activationSound.Play();
        }
    }
}