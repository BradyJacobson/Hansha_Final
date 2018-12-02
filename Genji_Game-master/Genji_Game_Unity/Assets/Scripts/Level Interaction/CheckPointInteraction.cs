using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointInteraction : MonoBehaviour
{
    public GameObject Player;
    public int Order;
    private bool activated;

    void Start()
    {
        activated = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && !activated)
        {
            Debug.Log("SET " + Order);
            PlayerPrefs.SetInt("checkpoint", Order);
            activated = true;
        }
    }
}