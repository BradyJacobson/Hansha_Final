using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Door : MonoBehaviour {

    public int startingHealth;

    private int _currentHealth;

    public void Start()
    {
        _currentHealth = startingHealth;
    }

    public void LoseCharacter()
    {
        _currentHealth -= 1;
        
        if (_currentHealth <= 0)
        {
            PlayerDeath();
            _currentHealth = 0;
        }
    }

    public void PlayerDeath()
    {
        Destroy(this.gameObject);
    }
}
