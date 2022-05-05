using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;
using System.Collections.Generic;

public class HandlerChestTiltForAllBodies : TrackerHandler
{
    public List<float> angles {private set; get;}
    public int NumOfBodies { private set; get;}

    private void Awake()
    {
        angles = new List<float>(20);

        for(int i = 0; i < 20; i++)
        {
            angles.Add(0);
        }
    }

    public override void updateTracker(BackgroundData trackerFrameData)
    {
        int closestBody = FindClosestTrackedBody(trackerFrameData);
        Body skeleton = trackerFrameData.Bodies[closestBody];

        NumOfBodies = (int)trackerFrameData.NumOfBodies;
        for (int i = 0; i < NumOfBodies; i++)
        {
            angles[i] =  calculateAngle(trackerFrameData.Bodies[i]);
        }
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
