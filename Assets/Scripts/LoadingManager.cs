using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static string nextScene;
    void Start()
    {
        StartCoroutine(LoadScene(nextScene));
    }
    private IEnumerator LoadScene(string scene)
    {
        yield return new WaitForSeconds(0.65f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
