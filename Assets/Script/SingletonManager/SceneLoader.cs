using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public Animator transAni;

    public void LoadScene(SceneType sceneType)
    {
        StartCoroutine(TransEffectSceneLoad(sceneType));
    }

    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneType currentType = (SceneType)Enum.Parse(typeof(SceneType), currentSceneName);
        StartCoroutine(TransEffectSceneLoad(currentType));
    }

    private IEnumerator TransEffectSceneLoad(SceneType scene)
    {
        transAni.updateMode = AnimatorUpdateMode.UnscaledTime;

        transAni.gameObject.SetActive(true);
        transAni.Play("Close");
        yield return new WaitForSecondsRealtime(0.1f);
        yield return new WaitForSecondsRealtime(transAni.GetCurrentAnimatorStateInfo(0).length);

        AsyncOperation op = SceneManager.LoadSceneAsync(scene.ToString());
        op.allowSceneActivation = false;

        while (op.progress < 0.9f) yield return null;

        ObjectPoolManager.instance.ClearAllPool();

        Time.timeScale = 0;        

        op.allowSceneActivation = true;

        while (!op.isDone) yield return null;
        yield return null;

        transAni.Play("Open");
        yield return null;
        yield return new WaitForSecondsRealtime(transAni.GetCurrentAnimatorStateInfo(0).length);
        transAni.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
