﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class MoveWithRightHand : MonoBehaviour
{
    public static Vector3 position;
    public GameObject BodySourceManager;
    // public Text TextIsClosed;

    SpriteRenderer _SpriteRenderer;
    BodySourceManager _BodyManager;
    float Solution_X = 960f;
    float Solution_Y = 640f;
    float Scalar_X = 19.2f;
    float Scalar_Y = 6.4f;
    bool isHandRightClosed = false;

    static readonly bool DEBUG = true;

    Dictionary<int, Mosquito> DestroyList = new Dictionary<int, Mosquito>();


    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG)
        {
            // Mouse Mode
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mp.x, mp.y);

            if (Input.GetMouseButtonDown(0))
            {
                if (!isHandRightClosed)
                {
                    isHandRightClosed = true;
                    Kill();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isHandRightClosed = false;
            }
        }
        else
        {
            if (BodySourceManager == null)
            {
                return;
            }

            _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
            if (_BodyManager == null)
            {
                return;
            }

            Kinect.Body[] bodies = _BodyManager.GetBodies();
            Kinect.CoordinateMapper coordinate = _BodyManager.GetCoordinate();

            if (bodies == null)
            {
                return;
            }

            if (bodies[0] == null)
            {
                return;
            }

            if (bodies[0].IsTracked)
            {
                Kinect.Joint rightHand = bodies[0].Joints[Kinect.JointType.HandRight];
                Kinect.CameraSpacePoint cameraSpacePoint = rightHand.Position;
                Kinect.ColorSpacePoint colorSpacePoint = coordinate.MapCameraPointToColorSpace(cameraSpacePoint);

                Vector3 HandPosition = new Vector2(Scalar_X * (colorSpacePoint.X - Solution_X) / Solution_X, -Scalar_Y * (colorSpacePoint.Y - Solution_Y) / Solution_Y);
                this.transform.position = HandPosition;

                if (bodies[0].HandRightState == Kinect.HandState.Closed && !isHandRightClosed)
                {
                    // TextIsClosed.text = "True";
                    isHandRightClosed = true;
                    _SpriteRenderer.sprite = Resources.Load<Sprite>("rich");
                    transform.localScale = new Vector3(3, 3, 1);
                }
                else if (bodies[0].HandRightState != Kinect.HandState.Closed && isHandRightClosed)
                {

                    // TextIsClosed.text = "False";
                    isHandRightClosed = false;
                    _SpriteRenderer.sprite = Resources.Load<Sprite>("#zpay");
                    transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }
            }
        }

        position = transform.position;
    }

    void OnTriggerEnter2D(Collider2D ColliderObj)
    {
        if (ColliderObj.gameObject.tag == "Mosquito")
        {
            Mosquito ms = ColliderObj.gameObject.GetComponent<Mosquito>();
            if (!DestroyList.ContainsKey(ms.mosquitoIndex)) DestroyList.Add(ms.mosquitoIndex, ms);
        }
    }

    public void Kill()
    {
        foreach (KeyValuePair<int, Mosquito> entry in DestroyList)
        {
            entry.Value.Kill();
        }
        DestroyList.Clear();
    }

    void OnTriggerLeave2D(Collider2D ColliderObj)
    {
        if (ColliderObj.gameObject.tag == "Mosquito")
        {
            DestroyList.Remove(ColliderObj.gameObject.GetComponent<Mosquito>().mosquitoIndex);
        }
    }

}
