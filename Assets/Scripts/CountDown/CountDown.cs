using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public Sprite[] Numbers=new Sprite[10];
    private double CountFrom = 0;
    private int NumberObjects = 0;
    private GameObject[] Displayer = new GameObject[3];
    private double accT;
    private double gap;
    private double rightPos;
    List<char> Decimals;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 curScale = transform.localScale;
        curScale.x = 1 / curScale.x;
        curScale.y = 1 / curScale.y;
        curScale.z = 1 / curScale.z;
        for (int i = 0; i < 3; ++i)
        {
            Displayer[i] = Instantiate(new GameObject("Count Down Displayer"), gameObject.transform);
            Displayer[i].AddComponent<SpriteRenderer>();
            Displayer[i].transform.localScale = curScale;
        }

        gap = Numbers[0].rect.width / 2 - 20;


        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        accT += Time.deltaTime;
        if (accT > 1)
        {
            CountFrom -= accT;
            if (CountFrom < 1)
            {
                gameObject.SetActive(false);
            }
            accT = 0;
            Decimals = Int2Decimal((int) CountFrom);
            if (NumberObjects != Decimals.Count)
            {
                NumberObjects = Decimals.Count;

                if (NumberObjects % 2 == 0)
                {
                    rightPos = gap * (NumberObjects / 2 - 1 + 0.5);
                }
                else
                {
                    rightPos = gap * ((NumberObjects - 1) / 2);
                }


                for(int i = 0; i < NumberObjects; ++i)
                {
                    Displayer[i].transform.localPosition = new Vector3( (float)(rightPos - i*gap), 0, -10);
                    Displayer[i].SetActive(true);
                }
                for(int i = NumberObjects; i < 3; ++i)
                {
                    Displayer[i].SetActive(false);
                }
            }

            for (int i = 0; i < NumberObjects; ++i)
            {
                Displayer[i].GetComponent<SpriteRenderer>().sprite = Numbers[Decimals[i]];
            }
        }

    }


    List<char> Int2Decimal(int value)
    {
        List<char> ret = new List<char>();
        if (value < 0) {
            ret.Add((char)0);
        }
        else
        {
            while (value > 0)
            {
                ret.Add((char)(value % 10));
                value /= 10;
            }
        }
        return ret;
    }

    public void SetCountDown(double value)
    {
        if (value < 0) CountFrom = 0;
        else if (value > 999) CountFrom = 999;
        else CountFrom = value;
        accT = 1.5;
        CountFrom += accT + 1 ;
        gameObject.SetActive(true);
    }
}


