using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Slash : MonoBehaviour
{

    [Header("Manual Setup")]
    //public GameObject backBoxCollider;
    //public GameObject midBoxCollider;
    public GameObject frontBoxCollider;
    public GameObject player;
    public GameObject deflectorMesh;

    [Header("UI Properties")]
    public GameObject Active;
    public GameObject Inactive;

    public float coolDown;
    public float swingLinger;

    private bool canSwing;
    private bool isSwinging;

    private AnimationScript animScript;

    // Use this for initialization
    void Start()
    {
        Inactive.SetActive(false);
        Active.SetActive(true);
        deflectorMesh.SetActive(false);
        frontBoxCollider.SetActive(false);
        canSwing = true;
        isSwinging = false;
        animScript = this.GetComponent<AnimationScript>();
        animScript.animScriptIsSwinging = this.isSwinging;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSwing)
        {
            frontBoxCollider.SetActive(true);
            Active.SetActive(false);
            Inactive.SetActive(true);
            canSwing = false;
            isSwinging = true;
            frontBoxCollider.transform.position = player.transform.position;
            StartCoroutine(SwingTime());
            GameObject.Find("Swing_Sounds").GetComponents<AudioSource>()[Random.Range(0, 3)].Play();
        }

        if (isSwinging)
        {
            frontBoxCollider.transform.position += (player.transform.forward * Time.deltaTime * 7);
            animScript.animScriptIsSwinging = this.isSwinging;
        }
    }

    IEnumerator SwingTime()
    {
        yield return new WaitForSeconds(swingLinger);
        frontBoxCollider.SetActive(false);
        deflectorMesh.SetActive(false);
        isSwinging = false;
        animScript.animScriptIsSwinging = this.isSwinging;
        StartCoroutine(SwingCoolDown());
    }

    IEnumerator SwingCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        canSwing = true;
        Inactive.SetActive(false);
        Active.SetActive(true);
    }
}
