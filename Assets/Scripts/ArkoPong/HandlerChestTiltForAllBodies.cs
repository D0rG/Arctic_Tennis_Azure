using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;
using System.Collections.Generic;

public class HandlerChestTiltForAllBodies : TrackerHandler
{
    public int NumOfBodies { private set; get; }
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
        if (trackerFrameData.NumOfBodies < 2)
        {
            Debug.LogWarning("я не могу найти два тела.");
            return;
        }

        bodies.Clear();
        for (int i = 0; i < (int)trackerFrameData.NumOfBodies; i++) //ƒобавл€ем все видные киннекту тела, дл€ далнейшей сортировки.
        {
            bodies.Add(trackerFrameData.Bodies[i]);
        }

        ShakerSort(bodies);
        RemoveNonPlayerBody(bodies);
        PelvisSort(bodies);

        for (int i = 0; i < bodies.Count; i++)
        {
            angles[i] = calculateAngleHand(bodies[i], (i == 0) ? PlayerSide.Left : PlayerSide.Right);
        }
    }

    /// <summary>
    /// Sort by distans to body.
    /// </summary>
    /// <param name="bodies"></param>
    public void ShakerSort(List<Body> bodies)
    {
        int left = 0;
        int right = bodies.Count - 1;
        int count = 0;

        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                ++count;
                if (FindBodyKinectDistance(bodies[i]) > FindBodyKinectDistance(bodies[i + 1]))
                {
                    bodies.Swap(i, i + 1);
                }
            }

            --right;
            for (int i = right; i > left; i--)
            {
                ++count;
                if (FindBodyKinectDistance(bodies[i - 1]) > FindBodyKinectDistance(bodies[i]))
                {
                    bodies.Swap(i - 1, i);
                }
            }
            ++left;
        }
    }

    /// <summary>
    /// Leaves only the two closest bodies.
    /// </summary>
    /// <param name="bodies"></param>
    private void RemoveNonPlayerBody(List<Body> bodies)
    {
        List<Body> tmp = new List<Body>(2);
        for (int i = 0; i < 2; i++)
        {
            tmp.Add(bodies[i]);
        }
        bodies = tmp;
    }

    /// <summary>
    /// Select side.
    /// </summary>
    /// <param name="bodies"></param>
    public void PelvisSort(List<Body> bodies)
    {
        var pelvisPositionFirst = bodies[0].JointPositions3D[(int)JointId.Pelvis];
        var pelvisPositionScnd = bodies[1].JointPositions3D[(int)JointId.Pelvis];

        if (pelvisPositionFirst.X > pelvisPositionScnd.X)
        {
            bodies.Swap(0, 1);
        }
    }

    private float calculateAngleHand(Body body, PlayerSide playerSide)
    {
        int sholderId;
        int wrist;
        int calculationDir = 1;

        if (playerSide == PlayerSide.Left)
        {
            sholderId = (int)JointId.ShoulderLeft;
            wrist = (int)JointId.WristLeft;
        }
        else
        {
            sholderId = (int)JointId.ShoulderRight;
            wrist = (int)JointId.WristRight;
            calculationDir = -1;
        }

        Vector2 sholderPos = body.JointPositions2D[sholderId].ToUnityVector2();
        Vector2 wirstPos = body.JointPositions2D[wrist].ToUnityVector2();

        Vector2 dirSide = new Vector2(sholderPos.x + calculationDir, sholderPos.y) - sholderPos;
        Vector2 dirToWirst = wirstPos - sholderPos;

        float angle = Vector2.SignedAngle(dirSide, dirToWirst);
        return angle * -1;
    }

    private enum PlayerSide
    {
        Left, Right
    }
}
