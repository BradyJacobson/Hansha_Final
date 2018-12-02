using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGuyEnemyAnimationScript : MonoBehaviour
{
    public Animator anim;


    public int currentHealth;

    public bool isRunning;

    void Start()
    {
        
    }

    private void Update()
    {
        if(isRunning)
        {
            anim.SetBool("bRunning", true);
        }
        else
        {
            anim.SetBool("bRunning", false);
        }
    }
}