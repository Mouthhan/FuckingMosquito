using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class MenuPlayerController : MonoBehaviour
{
    public static Vector3 position;
    public GameObject bodySourceManager;
    public Button StartButton;

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

    //animator
    Animator m_animator;
    AnimatorStateInfo stateInfo;
    bool isHoldStartButton = false;

    static readonly bool DEBUG = false;

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
        // for animation
        m_animator = gameObject.GetComponent<Animator>();
        m_animator.SetBool("handclosebool", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (DEBUG)
        {
            // Mouse Mode
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y);

            if (Input.GetMouseButtonDown(0) && !isHandRightClosed)
            {
                isHandRightClosed = true;
                m_animator.SetBool("handclosebool", true);
            }
            else if (Input.GetMouseButtonUp(0) && isHandRightClosed)
            {
                isHandRightClosed = false;
                m_animator.SetBool("handclosebool", false);
            }
        }
        else
        {
            // Fetch Body[] Information every time update() called
            bodies = bodyManager.GetBodies();

            if (bodies == null)
            {
                bodyID = -1;
            }
            else if (bodyID == -1 || bodies[bodyID].IsTracked == false)
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
            stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
            if (bodies[bodyID].HandRightState == Kinect.HandState.Closed && !isHandRightClosed)
            {
                isHandRightClosed = true;

                m_animator.SetBool("handclosebool", true);

                if (isHoldStartButton)
                {
                    StartButton.onClick.Invoke();
                }
            }
            else if (bodies[bodyID].HandRightState != Kinect.HandState.Closed && isHandRightClosed)
            {
                isHandRightClosed = false;

                m_animator.SetBool("handclosebool", false);
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

        GlobalVars.lastCursorPosition = GlobalVars.cursorPosition;
        GlobalVars.cursorPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D ColliderObj)
    {
        if (ColliderObj.gameObject.tag == "startbutton")
        {
            Debug.Log("碰撞");
            isHoldStartButton = true;
        }
    }

    void OnTriggerExit2D(Collider2D ColliderObj)
    {
        isHoldStartButton = false;
    }
}
