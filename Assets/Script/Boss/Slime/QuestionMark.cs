using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : MonoBehaviour, Iparryable
{
    public void OnParry()
    {
        Destroy(gameObject);
    }
}
