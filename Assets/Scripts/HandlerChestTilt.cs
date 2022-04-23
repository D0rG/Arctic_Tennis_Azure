using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.Azure.Kinect.BodyTracking;

public class HandlerChestTilt : TrackerHandler
{
    public float angle {private set; get;}
    public override void updateTracker(BackgroundData trackerFrameData)
    {
        int closestBody = FindClosestTrackedBody(trackerFrameData);
        Body skeleton = trackerFrameData.Bodies[closestBody];
        angle = calculateAngle(skeleton);
    }

    private float calculateAngle(Body body)
    {
        Vector2 pelvisPos = body.JointPositions2D[(int)JointId.Pelvis].ToUnityVector2();
        Vector2 chestPos = body.JointPositions2D[(int)JointId.SpineChest].ToUnityVector2();
        Vector2 directionUp = new Vector2(pelvisPos.x, pelvisPos.y + (-1)) - pelvisPos;
        Vector2 directionToChest = chestPos - pelvisPos;
        float angle = Vector2.SignedAngle(directionUp, directionToChest);
        Debug.Log(angle);
        return angle;
        //float angle = Vector2.Dot(directionUp, directionToChest) / (directionUp.magnitude * directionToChest.magnitude);
    }
}
