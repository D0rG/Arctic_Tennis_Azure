using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameRunner : MonoBehaviour
{
    public static GameRunner Instance { get; private set; }
    [SerializeField][Range(0, 10)] private int _waitTime;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
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
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        int timeToStart = _waitTime;
        while (true)
        {
            _text.text = timeToStart.ToString();
            yield return new WaitForSeconds(1);
            if (timeToStart > 0)
            {
                timeToStart--;
            }
            else
            {
                _text.text = string.Empty;
                OnStartMatch?.Invoke();
                yield break;
            }
        }
    }
}
