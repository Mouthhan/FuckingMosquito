using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] items = new GameObject[10];
    public double[] itemsEffectDistanceList = new double[10];
    public int[] itemsEffectTime = new int[10];
    public double itemExistTime = 0;

    private MainGameFunction scriptName;
    private int time = -1;
    private float randRangeX = 10, randRangeY = 5;  

    void Start()
    {
        scriptName = GameObject.Find("MainGameObj").GetComponent<MainGameFunction>();
        itemsEffectDistanceList[0] = 3;
        itemsEffectDistanceList[1] = 10;
        items[0].SetActive(false);
        items[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time = scriptName.time;
        //item appear.
        if (time % 5 == 0 && time < 60 && GlobalVars.itemUsedIndex == -1)
        {
            GlobalVars.itemUsedIndex = (int)Random.Range(0.5F, 1.5F);
            GlobalVars.itemIsUsed = false;
            items[GlobalVars.itemUsedIndex].transform.position = new Vector2(Random.Range(-randRangeX, randRangeX + 1), Random.Range(-randRangeY, randRangeY + 1));
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
}
