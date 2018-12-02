using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVidController : MonoBehaviour {

    public VideoPlayer introVideo;

    public GameObject menuCanvas;

	// Use this for initialization
	void Start ()
    {
        menuCanvas.SetActive(false);
        introVideo.loopPointReached += endVideo;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.anyKey)
        {
            endVideo(introVideo);
        }
	}

    void endVideo(VideoPlayer vp)
    {
        vp.Stop();
        menuCanvas.SetActive(true);
    }
}
