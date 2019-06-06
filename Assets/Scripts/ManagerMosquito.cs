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
        if (curMosquito < MaxMosquito)
        {
            Instantiate(MosquitoExample, new Vector3(3, 3, 0), MosquitoExample.transform.rotation).SetActive(true);
            curMosquito++;
        }

    }
}
