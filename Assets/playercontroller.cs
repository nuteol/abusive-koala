using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKey(KeyCode.A) /*&& rb.velocity.y == 0*/)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0); //character direction

        }
        if (Input.GetKey(KeyCode.D) /*&& rb.velocity.y == 0*/)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.W) && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            animator.SetBool("IsJumping", true);
        }
        if(Input.GetKey(KeyCode.S))
        {
            
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

}
