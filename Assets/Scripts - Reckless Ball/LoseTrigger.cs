using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoseTrigger : MonoBehaviour
{
    public static UnityAction OnLose;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnLose?.Invoke();
    }
}
