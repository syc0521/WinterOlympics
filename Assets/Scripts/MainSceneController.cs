using System.Collections;
using System.Collections.Generic;
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
}
