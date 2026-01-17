using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour, IPoolable
{
    private Animator anim;
    private AnimationHash<BulletEffectAniType> aniHash;
    public PoolItem PoolItemPre { get; set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        aniHash = new AnimationHash<BulletEffectAniType>(anim);
        //anim.updateMode
    }

    public void PlayEffect(BulletEffectAniType type)
    {
        aniHash.PlayFirstFrame(type);
        StopAllCoroutines();
        StartCoroutine(DespawnCo());
    }

    private IEnumerator DespawnCo()
    {
        yield return null;

        float dis = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(dis);

        PoolItemPre.Despawn();
    }

    public void OnSpawn() { }
    public void OnDespawn() => StopAllCoroutines();
}
