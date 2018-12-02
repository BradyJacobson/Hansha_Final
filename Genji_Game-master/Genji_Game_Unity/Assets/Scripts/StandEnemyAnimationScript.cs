using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandEnemyAnimationScript : MonoBehaviour
{
    public Animator anim;


    public int currentHealth;

    public bool isFiring;

    void Start()
    {
        
    }

    private void Update()
    {
        if(isFiring)
        {
            anim.SetBool("bFiring", true);
        }
        else
        {
            anim.SetBool("bFiring", false);
        }
    }
}