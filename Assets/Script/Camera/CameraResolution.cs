using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        float Aspect = 16.0f / 9.0f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / Aspect;

        Camera camera = GetComponent<Camera>();



        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = scaleHeight;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleHeight) / 2.0f;
            rect.y = 0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleWidth;
            rect.x = 0f;
            rect.y = (1.0f - scaleWidth) / 2.0f;

            camera.rect = rect;
        }
    }
}
