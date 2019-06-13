using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class HandClickEvent : MonoBehaviour
{
    [Serializable]
    public class ButtonHandClickedEvent : UnityEvent { }

    // Event delegates triggered on click.
    [FormerlySerializedAs("onHandClick")]
    [SerializeField]
    public ButtonHandClickedEvent onHandClick = new ButtonHandClickedEvent();
    
    void Start()
    {
    }

    void Update()
    {
    }
}
