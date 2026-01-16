using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using effectType;
using Unity.VisualScripting;

public class EffectAniController : MonoBehaviour
{
    private AnimationHash<EffectType> aniHash;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        aniHash = new AnimationHash<EffectType>(anim);
    }


}
