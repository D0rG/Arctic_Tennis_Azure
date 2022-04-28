using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;

public class DrawTrackerHandler : TrackerHandler
{
    //JointId enum have infrmation about index of body point. Like here https://docs.microsoft.com/en-us/azure/kinect-dk/body-joints
    const int pointsCountPerBody = 32;
    [SerializeField] private DimensionsMode dimensionsMode;
    private List<Transform> trackerPoints = new List<Transform>(pointsCountPerBody);
    
    private void Awake()
    {
        AddTrackers();
    }

    private void AddTrackers()
    {
        Vector3 size = new Vector3(0.1f, 0.1f, 0.1f);

        if (dimensionsMode == DimensionsMode.D2)
        {
            size *= 150;
        }

        for (int i = 0; i < pointsCountPerBody; i++)    //Add points for one more body
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Transform SphereTransform = sphere.GetComponent<Transform>();
            SphereTransform.localScale = size;
            trackerPoints.Add(SphereTransform);
        }
    }

    public override void updateTracker(BackgroundData trackerFrameData)
    {
        Debug.Log("Tracker frame data num bodies: " + trackerFrameData.NumOfBodies);
        for (int i = 0; i < (int)trackerFrameData.NumOfBodies; i++) //I can't use trackerFrameData.Bodies.Count because it is constantly equal to 20 
        {
            updatePositionPoints(trackerFrameData.Bodies[i], i);
        }
    }
    
    private void updatePositionPoints(Body body, int bodyNum = 0)
    {
        bodyNum++;
        for (int i = 0; i < pointsCountPerBody; i++)
        {
            Vector3 pointPosition;
            if (dimensionsMode == DimensionsMode.D3)
            {
                pointPosition = new Vector3(body.JointPositions3D[i].X, body.JointPositions3D[i].Y, body.JointPositions3D[i].Z);
            }
            else
            {
                pointPosition = new Vector3(body.JointPositions2D[i].X, body.JointPositions2D[i].Y, 0);
            }

            int index = i * bodyNum;
            Debug.Log($"{bodyNum}");

            if(index >= trackerPoints.Count)
            {
                AddTrackers();
            }

            trackerPoints[index].position = pointPosition;
        }
    }

    int findIndexFromId(BackgroundData frameData, int id)
    {
        int retIndex = -1;
        for (int i = 0; i < (int)frameData.NumOfBodies; i++)
        {
            if ((int)frameData.Bodies[i].Id == id)
            {
                retIndex = i;
                break;
            }
        }
        return retIndex;
    }

    private enum DimensionsMode
    {
        [InspectorName("2D tracking")]
        D2,
         [InspectorName("3D tracking")]
        D3
    }
}
