using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour 
{
    KinectSensor sensor;
    BodyFrameReader reader;
    Body[] bodies = null;
    CoordinateMapper coordinate;

    void Start () 
    {
        sensor = KinectSensor.GetDefault();

        if (sensor != null)
        {
            reader = sensor.BodyFrameSource.OpenReader();
            reader.FrameArrived += new System.EventHandler<BodyFrameArrivedEventArgs>(Reader_UpdateBodies);
            
            if (!sensor.IsOpen)
            {
                sensor.Open();
            }

            coordinate = sensor.CoordinateMapper;
        }   
    }
    
    void Update () 
    { 
    }

    /* Events */
    void Reader_UpdateBodies(object sender, BodyFrameArrivedEventArgs e)
    {
        using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
        {
            if (bodyFrame != null)
            {
                if (bodies == null)
                {
                    bodies = new Body[bodyFrame.BodyCount];
                }

                bodyFrame.GetAndRefreshBodyData(bodies);
                
                for (int i = 0; i < bodyFrame.BodyCount; ++i)
                {
                    if (bodies[i] == null)
                    {
                        Debug.Log("bodies[" + i.ToString() + "] == null");
                        continue;
                    }
                }
            }
        }
    }

    /* Access Functions */
    public Body[] GetBodies()
    {
        return bodies;
    }

    public CoordinateMapper GetCoordinate()
    {
        return coordinate;
    }

    /* Other Functions */
    void OnApplicationQuit()
    {
        if (reader != null)
        {
            reader.Dispose();
            reader = null;
        }
        
        if (sensor != null)
        {
            if (sensor.IsOpen)
            {
                sensor.Close();
            }
            
            sensor = null;
        }
    }
}
