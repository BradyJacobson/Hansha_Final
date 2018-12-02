using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator anim;

    public float speed;

    public int currentHealth;

    public bool animScriptIsSwinging;

    void Start()
    {
        
    }

    private void Update()
    {
        if (animScriptIsSwinging)
        {
            anim.SetBool("bSwinging", true);
        }
        else if (!animScriptIsSwinging)
        {
            anim.SetBool("bSwinging", false);
        }

        if (speed > 0.01)
        {
            anim.SetBool("bWalking", true);
            anim.SetFloat("fSpeed", speed / 10);
        }
        else if (speed <= 0.01)
        {
            anim.SetBool("bWalking", false);
            anim.SetFloat("fSpeed", speed);
        }

        if (currentHealth <= 0)
        {
            anim.SetBool("bDead", true);
        }
        else 
        {
            anim.SetBool("bDead", false);

        }
    }
}