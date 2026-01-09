using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            animator.Play(hash);
        }
    }
}
