using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;

[RequireComponent(typeof(ConfigLoader))]
public class main : MonoBehaviour
{
    // Handler for SkeletalTracking thread.
    [SerializeField] private TrackerProcessingMode TrackerMode;
    [SerializeField] private TrackerHandler[] m_tracker;
    private SkeletalTrackingProvider m_skeletalTrackingProvider;
    [HideInInspector] public BackgroundData m_lastFrameData = new BackgroundData();

    private void Start()
    {
#if !UNITY_EDITOR //Use config.json  file in build
        TrackerMode = GetConfigTrackerMode();
#endif

        //tracker ids needed for when there are two trackers
        const int TRACKER_ID = 0;
        m_skeletalTrackingProvider = new SkeletalTrackingProvider(TRACKER_ID, TrackerMode);
    }

    private void Update()
    {
        
        if (m_skeletalTrackingProvider.IsRunning)
        {
            if (m_skeletalTrackingProvider.GetCurrentFrameData(ref m_lastFrameData))
            {
                if (m_lastFrameData.NumOfBodies != 0)
                {
                    foreach(var handler in m_tracker)
                    {
                        if (handler != null)
                        {
                            handler.updateTracker(m_lastFrameData);
                        }
                    }
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (m_skeletalTrackingProvider != null)
        {
            m_skeletalTrackingProvider.Dispose();
        }
    }

    private TrackerProcessingMode GetConfigTrackerMode()
    {
        return ConfigLoader.Instance.Configs.TrackerMode;
    }

}
