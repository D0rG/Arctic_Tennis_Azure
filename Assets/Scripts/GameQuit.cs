using UnityEngine;

public class GameQuit : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }  
    }
}
