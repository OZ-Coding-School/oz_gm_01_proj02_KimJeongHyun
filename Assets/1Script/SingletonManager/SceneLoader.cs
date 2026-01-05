using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{

    public void LoadScene(EnumData.SceneType sceneType)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneType.ToString());
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AsyncOperation LoadSceneAsync(EnumData.SceneType sceneType)
    {
        Time.timeScale = 1;
        return SceneManager.LoadSceneAsync(sceneType.ToString());
    }
}
