using System;
using UnityEngine;
using System.Collections;


public class Follow_Script : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;    
    }

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}

