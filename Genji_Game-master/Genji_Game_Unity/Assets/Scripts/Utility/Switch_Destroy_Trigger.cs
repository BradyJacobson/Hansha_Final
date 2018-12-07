using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Destroy_Trigger : MonoBehaviour {

    public GameObject target;
    public bool willDestroy = false;
    public bool activeOnce = true;
    private bool activated = false;
   
    public void OnTriggerEnter(Collider collision)
    {

        Debug.Log("Register Collision", this);

        if(collision.gameObject.tag == "Player" && (activeOnce == true && activated == false))
        {
            Debug.Log("Collision is player", this);

            this.gameObject.transform.Rotate(0, 180, 0);

            GameObject.Find("Switch_Sound").GetComponent<AudioSource>().Play();

            Debug.Log("Move", this);

            if (willDestroy == true)
            {
                Destroy(target);
                Debug.Log("Destroyed target", this);
            }
            else
            {
                target.SetActive(false);
                Debug.Log("Deactivated target", this);
            }

            activated = true;
        }
    }
}
