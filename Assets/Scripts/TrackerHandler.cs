using UnityEngine;

public abstract class TrackerHandler : MonoBehaviour
{
    public abstract void updateTracker(BackgroundData trackerFrameData);
}
