using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class control_maincamera : MonoBehaviour
{
    public GameObject mos;
    public GameObject start_button;
    public GameObject canvas;
    public GameObject background;
    public AudioSource audioSource;
    // Start is called before the first frame update
    bool flag = false;

    GameObject maincamera;
    VideoPlayer video;

    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        video = maincamera.AddComponent<UnityEngine.Video.VideoPlayer>();
        video = maincamera.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (video.isPlaying)
        {
          mos.SetActive(false);
          start_button.SetActive(false);
          background.SetActive(false);
         flag = true;
        }
        if (!video.isPlaying)
        {
            mos.SetActive(true);
            start_button.SetActive(true);
            background.SetActive(true);
            
        }
        if (flag && !video.isPlaying)
        {
            video.targetCameraAlpha = 0.0f;
            if(!audioSource.isPlaying)
              audioSource.Play();
        }
    }
    void CheckOver(UnityEngine.Video.VideoPlayer video)
    {
        mos.SetActive(true);
        start_button.SetActive(true);
    }
}
