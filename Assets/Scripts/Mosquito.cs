﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Mosquito : MonoBehaviour
{

    //Location Variables
    private double x = 0;
    private double y = 0;
    private float scaleX, scaleY, scaleZ;

    //Environment Settings
    private const double GoBackDistance = 20;
    private const double PI = 3.1415926F;
    private double SpeedAngleConstant = 10F;

    //Speed Dependencies
    private double speed = 0F; // Move Length Per Second
    private double deltaSpeed = 0F;
    private double newSpeed = 0F;
    private double baseSpeed = 0.1F;
    private double maxSpeed = 15F;
    private double straightSpScaler = 30F;
    private double rotateSpScaler = 15F;
    private double maxAngleSpeed = 20F; //Test Purpose

    //Direction Dependencies
    private double direction = 0.0F;//In Radias
    private double deltadir = 0.01F; // In Radias
    private double newdeltadir = 0.01F;
    private double deltadeltadir = 0.0F;
    private double dirScaler= 20F;//In Angle
    private const double dirRange = 20F;//In Angle
    private const double dirRangeRad = dirRange * PI / 180F;
    private double straightdelta = PI * 3F / 180F; //inrange

    //Movement Dependencies
    private double moveDuration = 1.0F;
    private double transTime = 1.0F; //the time for a masquito to change it's speed and direction.
    private double baseDuration = 0.1F;
    private double durScale = 0.3F;

    //Counters
    private double AccuTime = 0;


    //Dangerous Dependencies
    private Vector3 lastCursorPosition = new Vector3(1000,1000, 10);
    private Vector3 cursor;
    private double dangerous3Position = 2;
    private double distance;
    private bool lastDangerous;


    //Animation Dependencies
    private Animator m_animator;
    private double deathAniLength;

    public Action[] foo = new Action[10];

    //Mosquito Informations
    private  bool alive;

    //ManagerMosquito Dependencies.
    ManagerMosquito mosManager;


    //Audios
    public AudioClip DeadSqeeze;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.rotation.z;
        moveDuration = 0.1f;
        newSpeed = 0.1f;
        deltaSpeed = newSpeed - speed;
        AccuTime = 0;
        alive = true;
        //Animation Setup
        m_animator = gameObject.GetComponent<Animator>();
        m_animator.SetBool("isdie", false);
        m_animator.SetBool("normal", true);
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
        foo[0] = weeds;
        foo[1] = bloodBaby;
        lastDangerous = false;
        //Kill();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVars.MainGameStop == 1)
        {
            return;
        }
        else if (!alive)
        {
            //yield return new WaitForSeconds(5);
            //deathAniLength -= Time.deltaTime;
            if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("new_die") &&   m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime>1)
            {
                Destroy(gameObject);
                GameObject.Find("MosquitoGenerator").GetComponent<ManagerMosquito>().Destroy();
            }
            return;
        }
        else
        {
            AccuTime += Time.deltaTime;

            direction = CheckRadias(direction + deltadir);

            //Movement
            x = transform.position.x;
            y = transform.position.y;
            double deltaPortion = Time.deltaTime / transTime;

            speed += deltaSpeed * deltaPortion;
            deltadir += deltadeltadir * deltaPortion;

            y += Mathf.Sin((float)direction) * speed * Time.deltaTime;
            x += Mathf.Cos((float)direction) * speed * Time.deltaTime;

            transform.position = new Vector3((float)x, (float)y, transform.position.z);

            if (direction > PI / 2.0 && direction < 1.5 * PI)
            {
                transform.localScale = new Vector3(scaleX,scaleY,scaleZ);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (GlobalVars.itemUsedIndex > -1 && GlobalVars.itemIsUsed && inItemEffectDistance())
            {
                Debug.Log("what?");
                foo[GlobalVars.itemUsedIndex]();
            }
            else if (isDangerous3())
            {
                if (!lastDangerous)
                {
                    ChangeToDangerous3();
                }
                dangerous3();
                lastDangerous = true;
            }
            else if (lastDangerous)
            {
                lastDangerous = false;
                ChangeToNormal();
            }
            else if (AccuTime >= moveDuration)
            {
                //Arrange New Behavior
                AccuTime = 0;
                if (Math.Pow(x * x + y * y, 0.5) > GoBackDistance)
                {  //Too far away from the center point
                   //Try to stay close to the center point
                    GoCenterBehavior();
                }
                else
                {
                    NormalBehavior();
                }

                if (newSpeed > maxSpeed) newSpeed = maxSpeed;
                deltaSpeed = newSpeed - speed;

                if (newdeltadir > dirRangeRad) newdeltadir = dirRangeRad;
                else if (newdeltadir < -dirRangeRad) newdeltadir = -dirRangeRad;
                deltadeltadir = newdeltadir - deltadir;
            }
        }
    }

    private double CheckRadias(double rad)
    {
        if(rad > 0)
        {
            rad = rad %(2* PI);
        }
        else{
            rad = -1 * Math.Abs(rad) %(2* PI);
        }
        return rad;
    }
 
    private void GoCenterBehavior()
    {
        moveDuration = 0.1;
        direction = CheckRadias(Math.Atan2(y, x) + PI);
        newdeltadir = 0;
        deltadir = 0;
        newSpeed = baseSpeed;
    }

    private void NormalBehavior()
    {
        moveDuration = GlobalVars.rand.NextDouble() * durScale + baseDuration;
        //Bias Random Direction
        double selector = GlobalVars.rand.NextDouble();
        if (selector < 0.2)
        {
            //Do Nothing, Keep The Delta Direction
        }
        else if (selector < 0.2 + 0.3)
        {
            //Select the Inverse of Delta Direction.
            newdeltadir = -newdeltadir;
        }
        else
        {
            //Randomly Select New Delta Direction
            newdeltadir = (Convert.ToSingle(GlobalVars.rand.NextDouble() - 0.5) * 2F * dirScaler) * PI / 180f;
            double deltadir_angle = newdeltadir * 180F / PI;
            if (deltadir_angle > dirRange)
            {
                newdeltadir = 0;
            }
            else if (deltadir_angle < -dirRange)
            {
                newdeltadir = 0;
            }
        }


        //Different speed in different modes.
        if (Math.Abs(newdeltadir) < straightdelta)
        {
            //If It Tends to move forward.
            newSpeed = GlobalVars.rand.NextDouble() * straightSpScaler + baseSpeed;
        }
        else
        {
            //If It Tends to turn.
            newSpeed = SpeedAngleConstant * Math.Abs(newdeltadir) + GlobalVars.rand.NextDouble() * rotateSpScaler + baseSpeed;
        }
    }

    void ChangeToNormal()
    {
        m_animator.SetBool("normal", true);
    }

    void ChangeToDangerous3()
    {
        m_animator.SetBool("normal", false);
    }

    bool isDangerous3()
    {
        Vector3 pos = transform.position;
        distance = Math.Sqrt(Math.Pow(GlobalVars.cursorPosition.x - pos.x, 2) + Math.Pow((GlobalVars.cursorPosition.y - pos.y), 2));
        double lastDistance = Math.Sqrt(Math.Pow(GlobalVars.lastCursorPosition.x - pos.x, 2) + Math.Pow((GlobalVars.lastCursorPosition.y - pos.y), 2));
        
        if (distance < dangerous3Position && distance < lastDistance)
        {
            return true;
        }
        return false;
    }

    void dangerous3()
    {
        AccuTime = 0;
        Vector2 transToCursor = new Vector2(transform.position.x - GlobalVars.cursorPosition.x, transform.position.y - GlobalVars.cursorPosition.y);
        Vector2 dir = new Vector2(Mathf.Cos((float)direction), Mathf.Sin((float)direction));
        double n = dangerous3Position - transToCursor.magnitude;
        direction = Vector2.SignedAngle(new Vector2(1, 0), dir + transToCursor * (float)n) / 180 * PI;
        if (speed < 2) speed = 2;
        deltadir = 0;
        deltadeltadir = 0;
    }

    public  void Kill()
    {
        if (alive)
        {
            GetComponent<AudioSource>().PlayOneShot(DeadSqeeze);
            alive = false;
            m_animator.SetBool("isdie",true);
        }
       
    }

    void weeds()
    {
        deltadir = UnityEngine.Random.Range(0.3f, 1f);
    }

    void bloodBaby()
    {
        Vector2 transToCursor = new Vector2(GlobalVars.cursorPosition.x - transform.position.x, GlobalVars.cursorPosition.y - transform.position.y);
        Vector2 dir = new Vector2(Mathf.Cos((float)direction), Mathf.Sin((float)direction));
        double n = (GlobalVars.itemEffectDistance - transToCursor.magnitude) / 5.0;
        direction = Vector2.SignedAngle(new Vector2(1, 0), dir + transToCursor * (float)n) / 180 * PI;
        if (speed < 1) speed = 1;
        deltadir = 0;
        deltadeltadir = 0;
    }

    bool inItemEffectDistance()
    {
        if (GlobalVars.Vector2Distance(transform.position, GlobalVars.cursorPosition) < GlobalVars.itemEffectDistance)
            return true;
        return false;
    }

}
