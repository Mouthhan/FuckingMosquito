using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
 
public class GlobalVars : MonoBehaviour
{
   
    public static System.Random rand = new System.Random();
    public static int MainGameStop;

    //cursor position
    public static Vector3 cursorPosition;
    public static Vector3 lastCursorPosition;

    public static GameObject start_button;

    //items
    public static int itemUsedIndex = -1;
    public static bool itemIsUsed = false;
    public static double itemEffectDistance = 0;
    public static double itemUsingTime = 0;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    //lastCursorPosition = new Vector3(1000, 1000, 10);
    //}

    // Update is called once per frame
    //void Update()
    //{


    //    //Debug.Log(cursorPosition);
    //}

    public static double Vector2Distance(Vector3 a,Vector3 b)
    {
        return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow((a.y - b.y), 2)); ;
    }
}
