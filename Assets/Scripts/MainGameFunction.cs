using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameFunction : MonoBehaviour
{
    public Text TextTime;
    public Text TextScore;
    public Text CountDown;

    public GameObject RestartButton;
    public GameObject QuitButton;
    public GameObject ResumeButton;
    public GameObject OverText;
    public GameObject TimeUI;
    public Canvas CountDownCanvas;
    private CountDown CountDownScript;

    public int time = -1;
    float time_f = 0f;
    float time_c = 0f;//倒數
    float count_down = 0;
    int score = 0;

    //item
    public GameObject[] items = new GameObject[10];
    public double[] itemsEffectDistanceList = new double[10];
    public int[] itemsEffectTime = new int[10];
    public double itemExistTime = 0;
    //public double itemUsingTime = 0;
     
    // Start is called before the first frame update
    void Start()
    {
        GlobalVars.MainGameStop = 0;
        OverText.SetActive(false);
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);


        CountDownScript = CountDownCanvas.GetComponent<CountDown>();
        ////initialize item effect
        //itemsEffectDistanceList[0] = 3;
        //itemsEffectDistanceList[1] = 10;
        //items[0].SetActive(false);
        //items[1].SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (count_down > 0)
        {
            count_down -= Time.deltaTime;

            if (count_down < 0)//取消暫停
            {
                GlobalVars.MainGameStop = 0;
                CountDown.text = "";
                time_c = 0f;
                count_down = -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
            ResumeButton.SetActive(true);
            GlobalVars.MainGameStop = 1;
        }
       else if (GlobalVars.MainGameStop == 0)
        {
            time_f += Time.deltaTime;
            time = 10 - (int)time_f;
            SetTime();
            // AddScore();
        }
        if (time == 0)
        {
            GlobalVars.MainGameStop = 1;
            OverText.SetActive(true);
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
        }
        
    }

    public void Resume()//resume的功能
    {
        Debug.Log("Resume 執行");
        count_down = 3;
        CountDownScript.SetCountDown(count_down);
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
    }


    public void Restart()//restart button的功能
    {
        Debug.Log("Restart 執行");
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
        //Initialize
        time_f = 0f;
        time = 0;
        score = 0;
        //Count Downs
        count_down = 3;
        CountDownCanvas.GetComponent<CountDown>().SetCountDown(count_down);
        //StartGame
        //GlobalVars.MainGameStop = 0;
    }


    public void SetTime()//時間
    {
        Vector3 test = TimeUI.transform.eulerAngles; test.z += Time.deltaTime*1000;
        TimeUI.transform.eulerAngles = test;
        if (time < 10)
        {
            TextTime.color = Color.red;
        }
        TextTime.text = time.ToString();
    }

    public void AddScore()//加分
    {
        score += 10;
        TextScore.text = score.ToString();
    }
}
