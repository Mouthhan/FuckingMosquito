using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public double effectDistance;
    public double effectTime;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D ColliderObj)
    {
        if (ColliderObj.gameObject.name == "newhand")
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
            
            GlobalVars.itemIsUsed = true;
            GlobalVars.itemEffectDistance = effectDistance;
            GlobalVars.itemUsingTime = effectTime;
        }
    }
}
