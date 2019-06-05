﻿using UnityEngine;
using UnityEngine.UI;
using Kinect = Windows.Kinect;

public class PlayerController : MonoBehaviour
{
    public static Vector3 position;
    public GameObject bodySourceManager;
    // public Text TextIsClosed;

    SpriteRenderer spriteRenderer;
    BodySourceManager bodyManager;

    Kinect.CoordinateMapper coordinate;

    Kinect.Body[] bodies;
    int bodyID = -1;

    // Screen Setting
    float solution_X = 960f;
    float solution_Y = 640f;
    float scalar_X = 19.2f;
    float scalar_Y = 6.4f;
    
    bool isHandRightClosed = false;
    bool isHandLeftClosed = false;

    // Mouse Position
    Vector3 mousePos;

    static readonly bool DEBUG = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!DEBUG)
        {
            // If there hasn't assigned BodySourceManager, byebye
            if (bodySourceManager == null)
                return;

            bodyManager = bodySourceManager.GetComponent<BodySourceManager>();
            // If there is no BodySourceManager
            if (bodyManager == null)
                return;

            coordinate = bodyManager.GetCoordinate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG)
        {
            // Mouse Mode
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y);
        }
        else
        {
            // Fetch Body[] Information every time update() called
            bodies = bodyManager.GetBodies();

            if (bodyID == -1 || bodies[bodyID].IsTracked == false)
            {
                // Finding a new bodyID
                bodyID = -1;
                for (int _i = 0; _i < bodies.Length; ++_i)
                {
                    if (bodies[_i].IsTracked)
                    {
                        bodyID = _i;
                        break;
                    }
                }
            }

            // If no any active body, byebye
            if (bodyID == -1)
                return;

            Kinect.CameraSpacePoint _cameraSpacePoint = bodies[bodyID].Joints[Kinect.JointType.HandRight].Position;
            Kinect.ColorSpacePoint _colorSpacePoint = coordinate.MapCameraPointToColorSpace(_cameraSpacePoint);

            transform.position = new Vector3(scalar_X * (_colorSpacePoint.X - solution_X) / solution_X, -scalar_Y * (_colorSpacePoint.Y - solution_Y) / solution_Y);

            if (bodies[bodyID].HandRightState == Kinect.HandState.Closed && !isHandRightClosed)
            {
                isHandRightClosed = true;
                spriteRenderer.sprite = Resources.Load<Sprite>("HandClose");
                transform.localScale = new Vector3(3, 3, 1);
            }
            else if (bodies[bodyID].HandRightState != Kinect.HandState.Closed && isHandRightClosed)
            {
                isHandRightClosed = false;
                spriteRenderer.sprite = Resources.Load<Sprite>("HandOpen");
                transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }

            if (bodies[bodyID].HandLeftState == Kinect.HandState.Closed && !isHandLeftClosed)
            {
                isHandLeftClosed = true;
            }
            else if (bodies[bodyID].HandLeftState != Kinect.HandState.Closed && isHandLeftClosed)
            {
                isHandLeftClosed = false;
            }
        }

        position = transform.position;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "item")
        {
            GlobalVars.itemIsUsed = true;
            GlobalVars.itemEffectDistance = 3;
            GlobalVars.itemUsingTime = 2;
        }
    }
}