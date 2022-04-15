using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.Kinect.Sensor;

public class Kinect : MonoBehaviour
{
    private Device kinect;

    private void Start()
    {
        InitKinect();
    }

    private void InitKinect()
    {
        kinect = Device.Open(0);
        kinect.StartCameras(new DeviceConfiguration
        {
            CameraFPS = FPS.FPS30,
            ColorResolution = ColorResolution.Off,
            DepthMode = DepthMode.NFOV_Unbinned,
            WiredSyncMode = WiredSyncMode.Standalone
        });
    }

    private void OnDestroy()
    {
        if(kinect != null)
        {
            kinect.StopCameras();
        }
    }
}
