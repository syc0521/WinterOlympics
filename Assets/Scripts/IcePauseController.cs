using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePauseController : MonoBehaviour
{
    public static bool isPaused;
    public AudioSource source;
    public GameObject lightPrefab;
    public GameObject pauseCanvas;
    private void Start()
    {
        //Instantiate(lightPrefab);
    }
    void Update()
    {
        PauseGame();
    }
    private void PauseGame()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = true;
                source.Pause();
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
