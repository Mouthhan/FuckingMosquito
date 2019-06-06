using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMosquito : MonoBehaviour
{
    const int MaxMosquito =10;
    private int curMosquito = 0;
    private Queue<int> NextMosquitoIndex = new Queue<int>();
    private GameObject[] Mosquitos = new GameObject[MaxMosquito];
   
    public GameObject MosquitoExample;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Destroy()
    {
        curMosquito -= 1;
    }

    // Update is called once per frame
     void Update()
    {
        GlobalVars.lastCursorPosition = GlobalVars.cursorPosition;
        GlobalVars.cursorPosition = Input.mousePosition;
        GlobalVars.cursorPosition.z = 20;
        GlobalVars.cursorPosition = Camera.main.ScreenToWorldPoint(GlobalVars.cursorPosition);

        if (curMosquito < MaxMosquito)
        {
            //if (NextMosquitoIndex.Count > 0)
            //{
            //    int index = NextMosquitoIndex.Dequeue();
            //    Mosquitos[index] = Instantiate(MosquitoExample, new Vector3(3, 3, 0), MosquitoExample.transform.rotation);
            //    Mosquitos[index].SetActive(true);
            //    Mosquitos[index].GetComponent<Mosquito>().mosquitoIndex = index;
            //}
            //else
            //{
            //    Mosquitos[curMosquito] = Instantiate(MosquitoExample, new Vector3(3, 3, 0), MosquitoExample.transform.rotation);
            //    Mosquitos[curMosquito].SetActive(true);
            //    Mosquitos[curMosquito].GetComponent<Mosquito>().mosquitoIndex = curMosquito;
            //}
            Instantiate(MosquitoExample, new Vector3(3, 3, 0), MosquitoExample.transform.rotation).SetActive(true);
            curMosquito++;
        }
        
    }
}
