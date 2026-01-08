using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationHash
{
    private readonly Animator animator;
    private readonly Dictionary<int, int> aniHash = new Dictionary<int, int>();

    public AnimationHash(Animator animator)
    {
        this.animator = animator;
    }

    public void InitAniHash<T>() where T : Enum
    {
        foreach (T state in Enum.GetValues(typeof(T)))
        {
            int key = state.GetHashCode();
            int hash = Animator.StringToHash(state.ToString());
            aniHash[key] = hash;
        }
    }

    public void PlayAni<T>(T state) where T : Enum
    {
        int key = state.GetHashCode();
        if (aniHash.TryGetValue(key, out int hash))
        {
            animator.Play(hash);
        }
    }
}
