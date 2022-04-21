using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;

public class CustomTrackerHandler : TrackerHandler
{
    //JointId enum have infrmation about index of body point. Like here https://docs.microsoft.com/en-us/azure/kinect-dk/body-joints
    const int pointsCount = 32;
    [SerializeField] private BodyDrawMode drawMode;
    [SerializeField] private DimensionsMode dimensionsMode;
    private List<Transform> trackerPoints = new List<Transform>(pointsCount);
    private List<Vector3> _pointsPosition = new List<Vector3>(pointsCount);
    public IReadOnlyList<Vector3> pointsPosition 
    {
       get
       {
           return _pointsPosition;
       }
    }
    
    private void Awake()
    {
        for(int i = 0; i < pointsCount; i++)
        {
            _pointsPosition.Add(Vector3.zero);
        }

        if (drawMode == BodyDrawMode.Draw)
        {
            if(trackerPoints.Count != 0)
            {
                foreach (var sphereTransform in trackerPoints)
                {
                    Destroy(sphereTransform.gameObject);
                }
                trackerPoints.Clear();
            }

            Vector3 size = new Vector3(0.1f, 0.1f, 0.1f);
            if(dimensionsMode == DimensionsMode.D2)
            {
                size *= 150;
            }
            SpawnSphere(size);
        }
    }

    private void SpawnSphere(Vector3 size, int count = 32)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Transform SphereTransform = obj.GetComponent<Transform>();
            SphereTransform.localScale = size;
            trackerPoints.Add(SphereTransform);
        }
    }

    public override void updateTracker(BackgroundData trackerFrameData)
    {
        //this is an array in case you want to get the n closest bodies
        int closestBody = findClosestTrackedBody(trackerFrameData);
        Body skeleton = trackerFrameData.Bodies[closestBody];
        
        updatePositionPoints(skeleton);
        if(drawMode == BodyDrawMode.Draw)
        {
            DrawBody();
        }
    }

    private void updatePositionPoints(Body body)
    {
        for (int i = 0; i < pointsCount; i++)
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

            _pointsPosition[i] = pointPosition;
        }
    }

    private void DrawBody()
    {
        for (int i = 0; i < pointsCount; i++)
        {
            trackerPoints[i].position = pointsPosition[i];
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

    private int findClosestTrackedBody(BackgroundData trackerFrameData)
    {
        int closestBody = -1;
        const float MAX_DISTANCE = 5000.0f;
        float minDistanceFromKinect = MAX_DISTANCE;
        for (int i = 0; i < (int)trackerFrameData.NumOfBodies; i++)
        {
            var pelvisPosition = trackerFrameData.Bodies[i].JointPositions3D[(int)JointId.Pelvis];
            Vector3 pelvisPos = new Vector3((float)pelvisPosition.X, (float)pelvisPosition.Y, (float)pelvisPosition.Z);
            if (pelvisPos.magnitude < minDistanceFromKinect)
            {
                closestBody = i;
                minDistanceFromKinect = pelvisPos.magnitude;
            }
        }
        return closestBody;
    }

    private enum BodyDrawMode
    {
        Draw,
        Hide
    }

    private enum DimensionsMode
    {
        [InspectorName("2D tracking")]
        D2,
         [InspectorName("3D tracking")]
        D3
    }
}
