using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    public Animation logoAni;
    public GameObject intro;
    public GameObject title;


    private AsyncOperation asy;
    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
        intro.SetActive(true);
        title.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadTitleCo());
    }

    private IEnumerator LoadTitleCo()
    {
        logoAni.Play();
        yield return new WaitForSeconds(logoAni.clip.length);
        intro.SetActive(false);
        title.SetActive(true);

        asy = SceneLoader.Instance.LoadSceneAsync(EnumData.SceneType.Lobby);
        if (asy == null)
        {
            yield break;
        }

        asy.allowSceneActivation = false;

        while(!asy.isDone)
        {

            if(asy.progress >= 0.9f)
            {
                //아무키나 누르세요 text 활성
                /*if (Input.anyKeyDown)
                {
                    asy.allowSceneActivation = true;
                    yield break;
                }*/
            }

            yield return null;
        }        
    }
}
