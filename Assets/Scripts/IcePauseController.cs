using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePauseController : MonoBehaviour
{
    public static bool isPaused;
    public AudioSource source;
    public GameObject lightPrefab;
    public GameObject pauseCanvas;
    public GameObject tutorialCanvas;
    private void Start()
    {
        //Instantiate(lightPrefab);
    }
    void Update()
    {
        PauseGame();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorialCanvas.SetActive(false);
        }
    }
    private void PauseGame()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                isPaused = true;
                source.volume = 0.2f;
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
