using System;
using UnityEngine;


public class Follow_Script : MonoBehaviour
{
    public Transform target;


    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}

