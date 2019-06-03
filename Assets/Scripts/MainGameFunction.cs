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
        RestartButton.SetActive(false);
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);

        //initialize item effect
        itemsEffectDistanceList[0] = 3;
        items[0].SetActive(false);
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
        //item appear.
        if(time % 5 == 0 && time < 60)
        {
            GlobalVars.itemUsedIndex = 0;
            GlobalVars.itemIsUsed = false;
            items[GlobalVars.itemUsedIndex].SetActive(true);
            GlobalVars.itemEffectDistance = itemsEffectDistanceList[GlobalVars.itemUsedIndex];
            itemExistTime = 2;
        }
        //item is used.
        if(GlobalVars.itemUsedIndex > -1){
            if(GlobalVars.itemIsUsed)
            {
                items[GlobalVars.itemUsedIndex].transform.position = GlobalVars.cursorPosition;
                if(GlobalVars.itemUsingTime > 0)
                {
                    Debug.Log("Using");
                    GlobalVars.itemUsingTime -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("Not Using");
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
