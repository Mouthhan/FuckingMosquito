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
    int flag = 1;//判斷是否運作
    // Start is called before the first frame update
    void Start()
    {
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
            flag = 0;
        }
        if (flag == 1)
        {
            time_f += Time.deltaTime;
            time = 60 - (int)time_f;
            SetTime();
            AddScore();
        }
    }
    public void Resume()//resume的功能
    {
        flag = 1;
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
    }
    public void Restart()//restart button的功能
    {
        flag = 1;
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
