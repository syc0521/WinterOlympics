using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public AudioSource BGM;
    public GameObject pauseCanvas;
    public void OnNextButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
        LoadingManager.nextScene = "Main";
    }
    public void OnContinueButtonPressed()
    {
        BGM.Play();
        Time.timeScale = 1;
        Snow.NoteController.isPaused = false;
        pauseCanvas.SetActive(false);
    }
    public void OnSnowRetryButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
        LoadingManager.nextScene = "Snow";
    }
    public void OnIceRetryButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Loading");
        LoadingManager.nextScene = "IceBall";
    }
}
