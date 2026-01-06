using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneUIController : BaseSceneUIController
{
    public Animation logoAni;
    public GameObject intro;
    public GameObject title;
    public GameObject pressText;

    protected override void Init()
    {
        base.Init();
        Screen.SetResolution(1280, 720, false);
        intro.SetActive(true);
        title.SetActive(false);
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(LoadTitleCo());
    }

    private IEnumerator LoadTitleCo()
    {        
        logoAni.Play();
        yield return new WaitForSeconds(logoAni.clip.length + 1.0f);
        intro.SetActive(false);        
        title.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        pressText.SetActive(true);

        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneLoader.Instance.LoadScene(EnumData.SceneType.Lobby);
    }
}
