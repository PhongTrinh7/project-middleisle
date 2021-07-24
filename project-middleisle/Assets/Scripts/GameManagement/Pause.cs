using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause PauseInstance;
    public bool GameIsPaused = false;

    private void Awake()
    {
        PauseInstance = this;
    }

    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
