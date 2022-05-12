using UnityEngine;
using UnityEngine.Events;

public class GameRunner : MonoBehaviour
{
    public static GameRunner Instance { get; private set; }
    [SerializeField][Range(0, 10)] private float waitTime;
    public UnityEvent OnStartMatch;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Invoke("StartMatch", waitTime);
    }
    
    private void StartMatch()
    {
        OnStartMatch.Invoke();
    }
}
