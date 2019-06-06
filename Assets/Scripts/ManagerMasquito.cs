using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMasquito : MonoBehaviour
{
    const int MaxMasquito =10;
    private int curMasquito = 0;
    private Queue<int> NextMosquitoIndex = new Queue<int>();
    private GameObject[] Masquitos = new GameObject[MaxMasquito];
   
    public GameObject MasquitoExample;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void Destroy(int index)
    {
        //NextMosquitoIndex.Enqueue(index);
        curMasquito -= 1;
    }

    // Update is called once per frame
     void Update()
    {
        if(curMasquito < MaxMasquito)
        {
            //if (NextMosquitoIndex.Count > 0)
            //{
            //    int index = NextMosquitoIndex.Dequeue();
            //    Masquitos[index] = Instantiate(MasquitoExample, new Vector3(3, 3, 0), MasquitoExample.transform.rotation);
            //    Masquitos[index].SetActive(true);
            //    Masquitos[index].GetComponent<Masquito>().mosquitoIndex = index;
            //}
            //else
            //{
            //    Masquitos[curMasquito] = Instantiate(MasquitoExample, new Vector3(3, 3, 0), MasquitoExample.transform.rotation);
            //    Masquitos[curMasquito].SetActive(true);
            //    Masquitos[curMasquito].GetComponent<Masquito>().mosquitoIndex = curMasquito;
            //}
            // Debug.Log("Create Mosquito");
            Instantiate(MasquitoExample, new Vector3(3, 3, 0), MasquitoExample.transform.rotation).SetActive(true);
            curMasquito++;
        }
        
    }
}
