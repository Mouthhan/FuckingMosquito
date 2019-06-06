using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    Dictionary<int, Masquito> DestroyList = new Dictionary<int, Masquito>();
    void OnTriggerEnter2D(Collider2D ColliderObj)
    {
        Masquito ms = ColliderObj.gameObject.GetComponent<Masquito>();
        if(!DestroyList.ContainsKey(ms.mosquitoIndex))DestroyList.Add(ms.mosquitoIndex, ms);
    }

    public void Kill()
    {
        foreach (KeyValuePair<int, Masquito> entry in DestroyList)
        {
            entry.Value.Kill();
        }
        DestroyList.Clear();
    }

    void OnTriggerLeave2D(Collider2D ColliderObj)
    {
        DestroyList.Remove(ColliderObj.gameObject.GetComponent<Masquito>().mosquitoIndex);
    }

}
