using UnityEngine;

public static class EffectHelper
{

    public static Quaternion GetEffectRot(Transform playerTransform)
    {
        return (playerTransform.localScale.x < 0) ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }

    public static void SetRandomEffect(Component effect, float minScale = 0.8f, float maxScale = 1.2f, float maxZRotate = 30f)
    {
        if (effect == null) return;
  

        float randomRange = Random.Range(minScale, maxScale);
        effect.transform.localScale *= randomRange;

        effect.transform.Rotate(0, 0, Random.Range(-maxZRotate, maxZRotate));

        if (Random.value > 0.5f)
        {
            Vector3 s = effect.transform.localScale;
            s.y *= -1;
            effect.transform.localScale = s;
        }
    }
}