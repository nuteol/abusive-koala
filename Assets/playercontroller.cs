using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) /*&& rb.velocity.y == 0*/)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D) /*&& rb.velocity.y == 0*/)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.W) && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }
}
