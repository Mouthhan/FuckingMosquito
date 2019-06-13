using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class intro_controller : MonoBehaviour
{
    public GameObject maincamera;
    public VideoPlayer video;
    public bool flag = false;
    float time_f = 0f;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        video = maincamera.AddComponent<UnityEngine.Video.VideoPlayer>();
        video = maincamera.GetComponent<UnityEngine.Video.VideoPlayer>();
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        time_f += Time.deltaTime;
        if (time_f >= 5)
        {
            video.targetCameraAlpha -= 0.001F;

        }
        
        if (video.isPlaying)
        {
            flag = true;
        }
        if (!video.isPlaying&&flag)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
