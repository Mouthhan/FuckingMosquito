using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class PlayerController : MonoBehaviour
{
    public static Vector3 position;
    public MainGameFunction mainGameFunction;
    public GameObject bodySourceManager;
    public GameObject background;
    
    BodySourceManager bodyManager;

    // Sprites
    Sprite kingChina;
    Sprite normalbackground;
    Sprite money;

    Kinect.CoordinateMapper coordinate;

    Kinect.Body[] bodies;
    int bodyID = -1;

    // Screen Setting
    float solution_X = 960f;
    float solution_Y = 640f;
    float scalar_X = 19.2f;
    float scalar_Y = 16.4f;
    
    bool isHandRightClosed = false;
    bool isHandLeftClosed = false;
    bool HandOnFace_China = false;
    bool isnormalbackground = true;
    bool isHandOnMoney = false;
    // Mouse Position
    Vector3 mousePos;

    //animator
    Animator m_animator;
    AnimatorStateInfo stateInfo;
    List<GameObject> DestroyList = new List<GameObject>();
    List<GameObject> ActiveUIButtonList = new List<GameObject>();

    static readonly bool DEBUG = false;

    void Start()
    {
        kingChina = Resources.Load<Sprite>("KingChina");
        normalbackground = Resources.Load<Sprite>("normalbackground");
        money = Resources.Load<Sprite>("money");

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
                KillMosquito();
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

               KillMosquito();
                
                Debug.Log("右手關起來ㄌ");

                if (ActiveUIButtonList.Count != 0)
                {
                    // Only invoke first ActiveUIButton
                    HandClickEvent e = ActiveUIButtonList[0].GetComponent<HandClickEvent>();
                    Debug.Log("觸發 UI_Button 事件");
                    e.onHandClick.Invoke();
                }
                ActiveUIButtonList.Clear();

                if (HandOnFace_China)
                {
                    isnormalbackground = false;
                    Debug.Log("換背景");
                    background.GetComponent<SpriteRenderer>().sprite = kingChina;
                }
                else if (isHandOnMoney)
                {
                    isnormalbackground = false;
                    Debug.Log("換背景");
                    background.GetComponent<SpriteRenderer>().sprite = money;
                }
            }
            else if (bodies[bodyID].HandRightState != Kinect.HandState.Closed && isHandRightClosed)
            {
                isHandRightClosed = false;

                m_animator.SetBool("handclosebool", false);
                Debug.Log("右手打開ㄌ");

                if (!isnormalbackground)
                {
                    background.GetComponent<SpriteRenderer>().sprite = normalbackground;
                }
            }

            if (bodies[bodyID].HandLeftState == Kinect.HandState.Closed && !isHandLeftClosed)
            {
                isHandLeftClosed = true;
                
                // 左手關閉表示進入 stop
                if(GlobalVars.MainGameStop != 1)
                    mainGameFunction.Stop();
            }
            else if (bodies[bodyID].HandLeftState != Kinect.HandState.Closed && isHandLeftClosed)
            {
                isHandLeftClosed = false;
            }
        }

        GlobalVars.lastCursorPosition = GlobalVars.cursorPosition;
        GlobalVars.cursorPosition = transform.position;
    }

    public void KillMosquito()
    {
        if (GlobalVars.MainGameStop == 1) return;
        foreach (GameObject obj in DestroyList)
        {
            Animator anm;
            anm = obj.GetComponent<Animator>();
            obj.GetComponent<Mosquito>().Kill();
            mainGameFunction.AddScore();
        }
        DestroyList.Clear();
    }

    void OnTriggerEnter2D(Collider2D ColliderObj)
    {
        if (ColliderObj.gameObject.tag == "Mosquito")
        {
            if (isHandRightClosed == false)
            {
                if (!DestroyList.Contains(ColliderObj.gameObject))
                {
                    DestroyList.Add(ColliderObj.gameObject);
                }
            }
        }
        else if (ColliderObj.gameObject.tag == "UI_Button")
        {
            Debug.Log("碰撞UI_Button");
            if (isHandRightClosed == false)
            {
                if (!ActiveUIButtonList.Contains(ColliderObj.gameObject))
                {
                    ActiveUIButtonList.Add(ColliderObj.gameObject);
                }
            }
        }

        if(ColliderObj.gameObject.tag == "MasterOfChina")
        {
            Debug.Log("碰撞維尼");
            HandOnFace_China = true;
        }
        else if (ColliderObj.gameObject.tag == "money")
        {
            Debug.Log("碰撞money");
            isHandOnMoney = true;
        }
    }

    void OnTriggerExit2D(Collider2D ColliderObj)
    {
        Debug.Log("離開");
        if (ColliderObj.gameObject.tag == "Mosquito")
        {
            DestroyList.Remove(ColliderObj.gameObject);
        }
        else if (ColliderObj.gameObject.tag == "UI_Button")
        {
            ActiveUIButtonList.Remove(ColliderObj.gameObject);
        }

        if (ColliderObj.gameObject.tag == "MasterOfChina")
        {
            Debug.Log("離開維尼");
            HandOnFace_China = false;
        }
        else if (ColliderObj.gameObject.tag == "money")
        {
            Debug.Log("碰撞money");
            isHandOnMoney = false;
        }
    }
}
