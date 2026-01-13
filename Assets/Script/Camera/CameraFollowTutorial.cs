using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTutorial : MonoBehaviour
{
    public Transform player;
    public Transform mask;
    public float speed = 5f;

    public float minX = 0f;
    public float maxX = 55.1f;

    private void LateUpdate()
    {
        float target = player.transform.position.x;
        float clampX = Mathf.Clamp(target, minX, maxX);

        Vector3 pos = new Vector3(clampX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        mask.position = new Vector3(transform.position.x, transform.position.y, mask.position.z);
    }
}
