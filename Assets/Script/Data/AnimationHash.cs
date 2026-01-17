using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AnimationHash<T> where T : Enum
{
    private readonly Animator animator;
    private readonly Dictionary<T, int> aniHash = new Dictionary<T, int>();

    public AnimationHash(Animator animator)
    {
        this.animator = animator;
        InitAniHash();
    }

    public void InitAniHash()
    {
        foreach (T state in Enum.GetValues(typeof(T)))
        {    
            aniHash.Add(state, Animator.StringToHash(state.ToString()));
        }
    }

    public void PlayAni(T state)
    {
        if (aniHash.TryGetValue(state, out var hash))
        {
            var curAni = animator.GetCurrentAnimatorStateInfo(0);
            if (curAni.shortNameHash != hash)
            {
                animator.Play(hash);
            }
        }
    }

    public void PlayAniSync(T state)
    {
        if (aniHash.TryGetValue(state,out var hash))
        {
            var curAni = animator.GetCurrentAnimatorStateInfo(0);
            if (curAni.shortNameHash != hash)
            {
                float normalizedTIme = curAni.normalizedTime % 1.0f;
                animator.Play(hash, 0, normalizedTIme);
            }
        }
    }

    public void PlayFirstFrame(T state)
    {
        if (aniHash.TryGetValue(state, out var hash))
        {
            animator.Play(hash, 0, 0);
        }
    }
}
