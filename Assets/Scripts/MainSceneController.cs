using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    public GameObject bgm;
    private void Start()
    {
        //bgm.GetComponent<AudioSource>().volume = 1f;
    }
    public void OnIceButtonPressed()
    {
        LoadingManager.nextScene = "IceBall";
        SceneManager.LoadScene("Loading");
    }

    public void OnSnowButtonPressed()
    {
        //bgm.GetComponent<BGM>().canDestroy = true;
        //StartCoroutine(bgm.GetComponent<BGM>().DestroyAudio());
        LoadingManager.nextScene = "Snow";
        SceneManager.LoadScene("Loading");
    }

    private void Update()
    {
        QuitProgram();
    }

    public static void QuitProgram()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else   
            Application.Quit();
#endif
        }
    }
}
