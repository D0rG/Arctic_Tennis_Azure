using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatrupSettings : MonoBehaviour
{
    [HideInInspector] public static StatrupSettings instance;   
    public Settings settings { private set; get; }
    [SerializeField] private string FileName;

    private void Awake()
    {
        instance = this;
        string path = Path.Combine(Application.dataPath, FileName);

        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Debug.Log(text);
            settings = JsonUtility.FromJson<Settings>(text);
        }
        else
        {
            settings = new Settings();  
            File.WriteAllText(path, JsonUtility.ToJson(settings));
        }
    }
}
