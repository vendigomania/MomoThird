using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public static bool IsGrounded;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
        Debug.Log("IsGrounded = " + IsGrounded);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        Debug.Log("IsGrounded = " + IsGrounded);
    }
}
