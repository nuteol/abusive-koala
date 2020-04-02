using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    public KeyCode equip1, attack1;

    public float runSpeed = 20f;
    public float jumpForce = 20f;
    float horizontalMove = 0f;

    //[SerializeField] private LayerMask ground;
    //[SerializeField] private LayerMask wall;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        if(horizontalMove > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (horizontalMove < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (rb.velocity.y == 0)
        {
            animator.SetBool("IsJumping", false);
        }
        if (Input.GetKey(KeyCode.W) && rb.velocity.y == 0)//coll.IsTouchingLayers(ground)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetKeyDown(equip1))
        {
            animator.SetBool("Weapon1Equipped", true);
        }
        if (Input.GetKeyDown(attack1))
        {
            animator.SetTrigger("Weapon1Attack");
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }
    void OnLanding()
    {
        
    }

}
