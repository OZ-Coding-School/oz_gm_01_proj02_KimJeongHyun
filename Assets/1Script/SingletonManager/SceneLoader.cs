using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public Animator transAni;
    public AnimationClip openAni;
    public AnimationClip closeAni;

    public void LoadScene(EnumData.SceneType sceneType)
    {
        StartCoroutine(TransEffectSceneLoad(sceneType));
    }

    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        EnumData.SceneType currentType = (EnumData.SceneType)Enum.Parse(typeof(EnumData.SceneType), currentSceneName);
        StartCoroutine(TransEffectSceneLoad(currentType));
    }

    private IEnumerator TransEffectSceneLoad(EnumData.SceneType scene)
    {
        Time.timeScale = 1.0f;

        transAni.gameObject.SetActive(true);
        transAni.Play(closeAni.name);
        yield return new WaitForSeconds(closeAni.length);

        AsyncOperation op = SceneManager.LoadSceneAsync(scene.ToString());
        op.allowSceneActivation = false;
        while (op.progress < 0.9f) yield return null;
        op.allowSceneActivation = true;
        while (!op.isDone) yield return null;

        transAni.Play(openAni.name);
        yield return new WaitForSeconds(openAni.length);
        transAni.gameObject.SetActive(false);
    }
}
