using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator anim;

    public float speed;

    public int currentHealth;

    public bool animScriptIsSwinging;

    private AudioSource[] moveSounds = new AudioSource[3];

    private bool isMoveSoundPlaying;

    private AudioSource lastSound;

    void Start()
    {
        moveSounds[0] = GameObject.Find("Movement_Sounds").GetComponents<AudioSource>()[0];
        moveSounds[1] = GameObject.Find("Movement_Sounds").GetComponents<AudioSource>()[1];
        moveSounds[2] = GameObject.Find("Movement_Sounds").GetComponents<AudioSource>()[2];
        isMoveSoundPlaying = false;
    }

    void Update()
    {
       // moveSounds[Random.Range(0, 3)].Play();
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
            if (!isMoveSoundPlaying)
            {
                PlaySound();
            }
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

    public void PlaySound()
    {
        moveSounds[Random.Range(0, 3)].Play();
        isMoveSoundPlaying = true;
        StartCoroutine(MoveSound());
    }

    IEnumerator MoveSound()
    {
        yield return new WaitForSeconds(0.20f);
        isMoveSoundPlaying = false;
    }
}