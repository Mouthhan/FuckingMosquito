using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningShow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SampleBlackBlock;
    private GameObject[,] BlackBlocks;
    private float eachwidth, eachheight;
    private float leftbotX, leftbotY;
    public float fadeTime = 5; //in seconds
    public float openSpeed;//in seconds
    private float nextOpen = 0;
    private int showX = 0;
    private int showY = 0;
    void Start()
    {
        openSpeed = fadeTime / 100;
        Rect rect =  gameObject.GetComponent<RectTransform>().rect;
        //Debug.Log(rect.width);
        //Debug.Log(rect.height);
        eachwidth = rect.width / 10;
        eachheight = rect.height / 10;
        leftbotX = -(rect.width / 2);
        leftbotY = -(rect.height / 2);
        //Debug.Log(leftbotX);
        //Debug.Log(leftbotY);
        SampleBlackBlock.GetComponent<RectTransform>().sizeDelta = new Vector2(eachwidth, eachheight);
        BlackBlocks = new GameObject[10, 10];
        for(int i = 0; i < 10; ++i)
        {
            for(int j = 0; j < 10; ++j)
            {
                BlackBlocks[i, j] = Instantiate(SampleBlackBlock,
                                                                  gameObject.transform);
                BlackBlocks[i, j].transform.localPosition = new Vector3(leftbotX + j * eachwidth, leftbotY + i * eachheight, 0);
                BlackBlocks[i, j].GetComponent<HiderAction>().showTime = openSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        nextOpen -= Time.deltaTime;
        if (nextOpen < 0)
        {
            if(showX < 10 && showY < 10)
            {

                BlackBlocks[showY, showX].GetComponent<HiderAction>().StartShow();

                if (showY%2 == 0)
                {
                    ++showX;
                }
                else
                {
                    --showX;
                }

                if (showX < 0)
                {
                    showX = 0;
                    ++showY;
                }
                else if(showX > 9)
                {
                    showX = 9;
                    ++showY;
                }

            }
            else
            {
                Destroy(gameObject);
            }
            nextOpen = openSpeed;
        }

    }
}

