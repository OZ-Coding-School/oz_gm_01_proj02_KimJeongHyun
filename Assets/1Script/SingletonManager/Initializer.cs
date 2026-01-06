using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Initializer : MonoBehaviour
{    
    public List<MonoBehaviour> managers;

    private void Start()
    {
        SceneLoader.Instance.LoadScene(EnumData.SceneType.Title);
        //StartCoroutine(LoadManager());
    }

    private IEnumerator LoadManager()
    {
        int totalManager = managers.Count;
        int readyCount = 0;
        
        while (readyCount < totalManager)
        {
            readyCount = 0;

            foreach (var manager in managers)
            {
                var field = manager.GetType().GetField("isReady");
                if (field != null && (bool)field.GetValue(manager) == true)
                {
                    readyCount++;
                }
            }
            yield return null;
        }

        SceneLoader.Instance.LoadScene(EnumData.SceneType.Title);
    }
}
