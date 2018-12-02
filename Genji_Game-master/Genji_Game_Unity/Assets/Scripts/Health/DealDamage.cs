using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

    public string playerTag = "Player";

    public int damageAmount = 10;
    public string damageFunctionName = "DealDamage";

    public bool continuousDamage = false;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.SendMessage(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnTriggerStay (Collider other)
    {
        if (continuousDamage && other.CompareTag(playerTag))
        {
            other.SendMessage(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
        }
    }
}
