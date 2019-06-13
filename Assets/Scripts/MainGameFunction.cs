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


    public Canvas CountDownCanvas;


    float time_f = 0f;
    int time = -1;
    float time_c = 0f;//倒數
    int count_down = -1;
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

        //initialize item effect
        itemsEffectDistanceList[0] = 3;
        itemsEffectDistanceList[1] = 10;
        items[0].SetActive(false);
        items[1].SetActive(false);


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
            // AddScore();
        }
        if (time == 0)
        {
            GlobalVars.MainGameStop = 1;
            OverText.SetActive(true);
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
        }
        if (count_down != -1)
        {
            if (count_down == 0)//執行
            {
                GlobalVars.MainGameStop = 0;
                CountDown.text = "";
                time_c = 0f;
                count_down = -1;
            }
            else
            {
                time_c += Time.deltaTime;
                count_down = 3 - (int)time_c;
               // CountDown.text = count_down.ToString();
            }
        }
        //item appear.
        if (time % 5 == 0 && time < 60 && GlobalVars.itemUsedIndex == -1)
        {
            GlobalVars.itemUsedIndex = (int)Random.Range(0, 2);
            GlobalVars.itemIsUsed = false;
            items[GlobalVars.itemUsedIndex].SetActive(true);
            GlobalVars.itemEffectDistance = itemsEffectDistanceList[GlobalVars.itemUsedIndex];
            itemExistTime = 2;
        }
        
        if(GlobalVars.itemUsedIndex > -1){
            //item is used.
            if(GlobalVars.itemIsUsed)
            {
                items[GlobalVars.itemUsedIndex].transform.position = GlobalVars.cursorPosition;
                if(GlobalVars.itemUsingTime > 0)
                {
                    GlobalVars.itemUsingTime -= Time.deltaTime;
                }
                else
                {
                    items[GlobalVars.itemUsedIndex].SetActive(false);
                    GlobalVars.itemIsUsed = false;
                    GlobalVars.itemEffectDistance = 0;
                    GlobalVars.itemUsedIndex = -1;
                }
            }
            //still didn't get item
            else
            {
                if(itemExistTime > 0)
                {
                    itemExistTime -= Time.deltaTime;
                }
                //didn't get item, let it disappear
                else
                {
                    items[GlobalVars.itemUsedIndex].SetActive(false);
                    GlobalVars.itemIsUsed = false;
                    GlobalVars.itemEffectDistance = 0;
                    GlobalVars.itemUsedIndex = -1;
                }
            }
        }
    }

    public void Resume()//resume的功能
    {
        count_down = 3;
        CountDownCanvas.GetComponent<CountDown>().SetCountDown(count_down);
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
    }
    public void Restart()//restart button的功能
    {
        Debug.Log("Log");
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
