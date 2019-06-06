using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderAction : MonoBehaviour
{
    // Start is called before the first frame update
    CanvasRenderer cr;
    bool started;
    float timeLeft;
    public float showTime;
    Color newColor;
    void Start()
    {
        cr = GetComponent<CanvasRenderer>();
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        Show();
    }

    void Show()
    {
        if (started)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                //newColor = material.color;
                //newColor.a = 
                //material.color = newColor;
                cr.SetAlpha(timeLeft / showTime);
            }
        }
    }

    public void StartShow()
    {
        started = true;
        timeLeft = showTime;
    }
}
