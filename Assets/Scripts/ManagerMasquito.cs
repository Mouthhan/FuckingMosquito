using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMasquito : MonoBehaviour
{
    const int MaxMasquito =0;
    private int curMasquito = 0;
    private GameObject[] Masquitos = new GameObject[MaxMasquito];
    public GameObject MasquitoExample;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // GlobalVars.lastCursorPosition = GlobalVars.cursorPosition;
        // GlobalVars.cursorPosition = Input.mousePosition;
        // GlobalVars.cursorPosition.z = 20;
        // GlobalVars.cursorPosition = Camera.main.ScreenToWorldPoint(GlobalVars.cursorPosition);

        if (curMasquito < MaxMasquito)
        {
            Masquitos[curMasquito] =Instantiate( MasquitoExample, new Vector3(3, 3, 0),MasquitoExample.transform.rotation);
            Masquitos[curMasquito].SetActive( true);
            curMasquito++;
        }
        
    }
}
