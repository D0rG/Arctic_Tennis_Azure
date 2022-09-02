using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;
using System.Collections.Generic;

public class HandlerChestTiltForAllBodies : TrackerHandler
{
    public int NumOfBodies { private set; get;}
    private List<Body> bodies = new List<Body>(20);
    public List<float> angles { private set; get; }

    private void Awake()
    {
        angles = new List<float>(2);
        for (int i = 0; i < 2; i++)
        {
            angles.Add(0);
        }
    }

    public override void updateTracker(BackgroundData trackerFrameData)
    {
        bodies.Clear();
        for (int i = 0; i < (int)trackerFrameData.NumOfBodies; i++)
        {
            bodies.Add(trackerFrameData.Bodies[i]);
        }
        
        ShakerSort(bodies);
        PelvisSortAndClearList(bodies);

        for (int i = 0; i < 2; i++)
        {
            angles[i] = calculateAngle(bodies[i]);
        }
    }

    public void ShakerSort(List<Body> bodies)
    {
        int left = 0;
        int right = bodies.Count - 1;
        int count = 0;

        while (left < right)
        {
            for(int i = left; i < right; i++)
            {
                ++count;
                if (FindBodyKinectDistance(bodies[i]) > FindBodyKinectDistance(bodies[i + 1]))
                {
                    Swap(ref bodies, i, i + 1);
                }
            }

            --right;
            for (int i = right; i > left; i--)
            {
                ++count;
                if(FindBodyKinectDistance(bodies[i - 1]) > FindBodyKinectDistance(bodies[i]))
                {
                    Swap(ref bodies, i - 1, i);
                }
            }
            ++left;
        }
    }

    public void PelvisSortAndClearList(List<Body> bodies)
    {
        if (bodies.Count >= 2)
        {
            Debug.Log("Two or more bodies");
            List<Body> tmp = new List<Body>(2);

            for (int i = 0; i < 2; i++)
            {
                tmp.Add(bodies[i]);
            }

            var pelvisPositionFirst = tmp[0].JointPositions3D[(int)JointId.Pelvis];
            var pelvisPositionScnd = tmp[1].JointPositions3D[(int)JointId.Pelvis];

            if(pelvisPositionFirst.X > pelvisPositionScnd.X)
            {
                Swap(ref tmp, 0, 1);
            }

            bodies = tmp;
        }
        else
        {
            Debug.Log("Cant find two or more bodies");
        }
    }

    public void Swap<T>(ref List<T> listed, int x, int y)
    {
        T temp = listed[x];
        listed[x] = listed[y];
        listed[y] = temp;  
    }

    private float calculateAngle(Body body)
    {
        Vector2 pelvisPos = body.JointPositions2D[(int)JointId.Pelvis].ToUnityVector2();
        Vector2 chestPos = body.JointPositions2D[(int)JointId.SpineChest].ToUnityVector2();
        Vector2 directionUp = new Vector2(pelvisPos.x, pelvisPos.y + (-1)) - pelvisPos;
        Vector2 directionToChest = chestPos - pelvisPos;
        float angle = Vector2.SignedAngle(directionUp, directionToChest);
        return -angle;
    }
}
