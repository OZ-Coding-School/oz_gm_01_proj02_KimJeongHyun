using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollowTutorial : MonoBehaviour
{
    public Transform player;
    public Transform mask;
    public float speed = 5f;

    public float minX = 0f;
    public float maxX = 55.1f;

    public bool useMap = false;
    public float minY = 0;
    public float maxY = 20f;

    private void LateUpdate()
    {
        float targetX = Mathf.Clamp(player.position.x, minX, maxX);
        float targetY = player.transform.position.y;

        if (useMap)
        {
            targetY = Mathf.Clamp(player.position.y, minY, maxY);
        }

        Vector3 pos = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        if (mask != null)
        {
            mask.position = new Vector3(transform.position.x, transform.position.y, mask.position.z);
        }
    }
}
