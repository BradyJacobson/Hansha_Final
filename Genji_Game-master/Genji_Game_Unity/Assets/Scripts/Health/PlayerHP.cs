using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public GameObject Player, Camera;
    public int startingHealth = 30;

    public UnityEvent damageEvent;
    public UnityEvent deathEvent;

    public int _currentHealth;
    private bool _canTakeDamage = true;

    public float InvincibilityTime;
    public float DeathAnimTime;

    private int CurrentCheckpoint;

    [Header("UI Properties")]
    public GameObject Health3;
    public GameObject Health2;
    public GameObject Health1;

    public string RestartScene;

    private AnimationScript animScript;

    public List<GameObject> Checkpoints;

    public void Start()
    {
        _currentHealth = startingHealth;
        InvincibilityTime = 2f;
        CurrentCheckpoint = PlayerPrefs.GetInt("checkpoint");
        animScript = this.GetComponent<AnimationScript>();
        animScript.currentHealth = _currentHealth;
        if (Checkpoints[CurrentCheckpoint])
        {
            Player.transform.position = Checkpoints[CurrentCheckpoint].transform.position;
            Camera.transform.position = new Vector3(Checkpoints[CurrentCheckpoint].transform.position.x, Checkpoints[CurrentCheckpoint].transform.position.y + 16, Checkpoints[CurrentCheckpoint].transform.position.z - 8f);
        }
        else
        {
            Player.transform.position = Checkpoints[0].transform.position;
            Camera.transform.position = new Vector3(Checkpoints[0].transform.position.x, Checkpoints[CurrentCheckpoint].transform.position.y + 16, Checkpoints[0].transform.position.z);
        }
    }

    void Update()
    {
        animScript.currentHealth = _currentHealth;
        if (Input.GetKeyDown(KeyCode.R) && _currentHealth > 0)
        {
            PlayerPrefs.SetInt("checkpoint", 0);
            SceneManager.LoadScene(RestartScene);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _currentHealth > 0)
        {
            SceneManager.LoadScene("_Main_Menu");
        }
    }

    public void DealDamage(int damage)
    {
        if (!_canTakeDamage)
            return;

        _currentHealth -= damage;

        damageEvent.Invoke();
        if (_currentHealth >= 21)
        {
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(true);
            _canTakeDamage = false;
            StartCoroutine(Invincible());
        }
        else if (_currentHealth >= 11)
        {
            Health1.SetActive(false);
            Health2.SetActive(true);
            Health3.SetActive(true);
            _canTakeDamage = false;
            StartCoroutine(Invincible());
        }
        else if (_currentHealth >= 1)
        {
            Health1.SetActive(false);
            Health2.SetActive(false);
            Health3.SetActive(true);
            _canTakeDamage = false;
            StartCoroutine(Invincible());
        }
        else 
        {
            Health1.SetActive(false);
            Health2.SetActive(false);
            Health3.SetActive(false);
            this.PlayerDeath();
            _currentHealth = 0;
        }
    }

    public void PlayerDeath()
    {
        deathEvent.Invoke();

        _canTakeDamage = false;

        StartCoroutine(Death());
    }

    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(InvincibilityTime);
        _canTakeDamage = true;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(DeathAnimTime);
        Debug.Log("Death() was called");
        SceneManager.LoadScene(RestartScene);
    }
}