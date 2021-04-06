using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    public void OnIceButtonPressed()
    {

    }

    public void OnSnowButtonPressed()
    {
        SceneManager.LoadScene("Snow");
    }
}
