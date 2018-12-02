using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInteraction : MonoBehaviour
{
    public GameObject Health2, Health3, Player;
    private int CurrentHealth;

	public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CurrentHealth = Player.GetComponent<PlayerHP>()._currentHealth;

            if (CurrentHealth < 21)
            {
                CurrentHealth += 10;
                Player.GetComponent<PlayerHP>()._currentHealth = CurrentHealth;
                if (CurrentHealth > 20)
                {
                    Health3.SetActive(true);

                }
                else if (CurrentHealth > 10)
                {
                    Health2.SetActive(true);
                }
                    Destroy(this.gameObject);
            }
        }
    }
}