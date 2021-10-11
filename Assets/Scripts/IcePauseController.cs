using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePauseController : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseCanvas;
    public GameObject tutorialCanvas;
    public BGMController bgm;
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
                bgm.IsActive = false;
                isPaused = true;
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

}
