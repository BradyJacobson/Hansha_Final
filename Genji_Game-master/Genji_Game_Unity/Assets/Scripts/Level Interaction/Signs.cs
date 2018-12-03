using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signs : MonoBehaviour
{
    public GameObject Description;
    public bool Active;

    void Start()
    {
        Active = false;
    }
    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Active == false)
        {
            Description.SetActive(true);
            Active = true;
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Active == true)
        {
            Description.SetActive(false);
            Active = false;
        }
    }
}
