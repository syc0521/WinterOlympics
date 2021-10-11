using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public AudioSource BGM;
    public BGMController source;
    public GameObject pauseCanvas;
    public void OnNextButtonPressed()
    {
        Snow.NoteController.isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
        LoadingManager.nextScene = "Main";
    }
    public void OnContinueButtonPressed()
    {
        Snow.NoteController.isPaused = false;
        BGM.Play();
        Time.timeScale = 1;
        Snow.NoteController.isPaused = false;
        pauseCanvas.SetActive(false);
    }
    public void OnSnowRetryButtonPressed()
    {
        Snow.NoteController.isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
        LoadingManager.nextScene = "Snow";
    }
    public void OnIceNextButtonPressed()
    {
        IcePauseController.isPaused = false;
        Time.timeScale = 1;
        BGM.volume = 1;
        SceneManager.LoadScene("Loading");
        LoadingManager.nextScene = "Main";
    }
    public void OnIceContinueButtonPressed()
    {
        Time.timeScale = 1;
        IcePauseController.isPaused = false;
        pauseCanvas.SetActive(false);
        source.IsActive = true;
    }
}
