using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string NextScene;
    public bool Reset;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(Reset)
            {
                PlayerPrefs.SetInt("checkpoint", 0);
            }
            SceneManager.LoadScene(NextScene);
        }
    }
}
