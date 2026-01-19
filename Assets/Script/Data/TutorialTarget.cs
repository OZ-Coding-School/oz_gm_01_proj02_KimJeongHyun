using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour, IDamageable
{
    [SerializeField] FlashEffect effect;
    float hp = 10;
    
    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnDamage(float dmg, Vector2 hitDir)
    {
        hp -= dmg;
        effect.PlayFlash();
    }
}
