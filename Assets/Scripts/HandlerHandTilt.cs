using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;

public class HandlerHandTilt : TrackerHandler
{
    public float angle { private set; get; }
    public override void updateTracker(BackgroundData trackerFrameData)
    {
        int closestBody = FindClosestTrackedBody(trackerFrameData);
        Body skeleton = trackerFrameData.Bodies[closestBody];
        angle = calculateAngle(skeleton);
    }
    
    private float calculateAngle(Body body)
    {
        int sholderId;
        int wrist;
        int dir;

        if (true)
        {
            sholderId = (int)JointId.ShoulderLeft;
            wrist = (int)JointId.WristLeft;
            dir = 1;
        }
        else
        {
            sholderId = (int)JointId.ShoulderRight;
            wrist = (int)JointId.WristRight;
            dir = -1;
        }

        Vector2 sholderPos = body.JointPositions2D[sholderId].ToUnityVector2();
        Vector2 wirstPos = body.JointPositions2D[wrist].ToUnityVector2();

        Vector2 dirSide = new Vector2(sholderPos.x + dir, sholderPos.y) - sholderPos;
        Vector2 dirToWirst = wirstPos - sholderPos;

        float angle = Vector2.SignedAngle(dirSide, dirToWirst);
        Debug.Log(angle);
        return angle;
    }


}