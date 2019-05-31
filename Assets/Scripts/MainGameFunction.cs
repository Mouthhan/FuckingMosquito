using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameFunction : MonoBehaviour
{
    public Text TextTime;
    public Text TextScore;
    public GameObject RestartButton;
    public GameObject QuitButton;
    public GameObject ResumeButton;
    float time_f = 0f;
    int time = 0;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        GlobalVars.MainGameStop = 0;
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
            ResumeButton.SetActive(true);
            GlobalVars.MainGameStop = 1;
        }
        if (GlobalVars.MainGameStop == 0)
        {
            time_f += Time.deltaTime;
            time = 60 - (int)time_f;
            SetTime();
            AddScore();
        }
    }
    public void Resume()//resume的功能
    {
        GlobalVars.MainGameStop = 0;
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
    }
    public void Restart()//restart button的功能
    {
        GlobalVars.MainGameStop = 0;
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
        time_f = 0f;
        time = 0;
        score = 0;
    }
    public void SetTime()//時間
    {
        TextTime.text = "Time: ";
        TextTime.text += time.ToString();
    }

    public void AddScore()//加分
    {
        score += 10;
        TextScore.text = "Score: ";
        TextScore.text += score.ToString();
    }
}
