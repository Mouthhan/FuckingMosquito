using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyFriday : MonoBehaviour
{
    float i = 10f;
    int flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flag==0)
        {
            for(int j=0;j<700;j++)
            {
                transform.rotation = Quaternion.Euler(0, 0, i);
            }
            flag = 1;
        }
      else if(flag==1)
        {
            i *= -1;
            for (int j = 0; j < 700; j++)
            {
                transform.rotation = Quaternion.Euler(0, 0, i);
            }
            i *= -1;
            flag = 0;
        }
        
    }
}
