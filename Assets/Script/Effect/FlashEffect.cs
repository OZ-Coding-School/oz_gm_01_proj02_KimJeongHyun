using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    private static readonly int FlashAmountProp = Shader.PropertyToID("_FlashAmount");

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void PlayFlash(float val = 0.2f)
    {
        StartCoroutine(Flash(val));
    }

    private IEnumerator Flash(float val)
    {
        sr.material.SetFloat(FlashAmountProp, 0.8f);
        yield return new WaitForSeconds(val);
        sr.material.SetFloat(FlashAmountProp, 0f);
    }
}
