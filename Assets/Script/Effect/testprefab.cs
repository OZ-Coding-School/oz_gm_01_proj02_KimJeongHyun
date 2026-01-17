using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testprefab : MonoBehaviour, Iparryable
{
    public void OnParry()
    {
        Destroy(gameObject);
    }
}
