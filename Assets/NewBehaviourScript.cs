using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Text TextTime;

    void Start()
    { }

    void Update()
    { }

    /*
    public Text ScoreText;
    public Text TimeText;
    public int Score = 0;
    public float timer_f = 0f;
    public int time = 0;
    public int time_limit = 60;
    public static NewBehaviourScript Instance;
    public GameObject StartButton;
    public GameObject RestartButton;
    
    public void StartGame() //StartButton的功能
    {
        Instance = this;
        Application.LoadLevel(Application.loadedLevel); //讀取關卡(已讀取的關卡)
    }

    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        time_limit = 60;
        RestartButton.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        timer_f += Time.deltaTime;
        time = time_limit - (int)timer_f;
        Debug.Log(time);

    }
    public void timer()
    {

    }
    public void AddScore()
    {
        Score += 10; //分數+10
        ScoreText.text = "Score: " + Score; // 更改ScoreText的內容
    }
    */
}
