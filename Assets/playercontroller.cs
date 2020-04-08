using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    private int maxHearts = 3;
    private int currentHearts = 3;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    private Transform player;
    public KeyCode equip1, attack1;

    public float runSpeed = 20f;
    public float jumpForce = 20f;
    float horizontalMove = 0f;

    bool lockUp = false;
    bool lockDown = false;
    bool lockLeft = false;
    bool lockRight = false;

    //[SerializeField] private LayerMask ground;
    //[SerializeField] private LayerMask wall;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        player = GetComponent<Transform>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (horizontalMove > 0)
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
        if (Input.GetKey(KeyCode.W) && rb.velocity.y == 0 && !lockUp)//coll.IsTouchingLayers(ground) //rb.velocity.y <= 1.192094e-07 && rb.velocity.y >= -1.192094e-07
        {
            lockUp = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetKey(KeyCode.A) && !lockLeft)
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            lockRight = false;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D) && !lockRight)
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            lockLeft = false;
        }
        else
        {
            if(!Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Death"))
        {
            Death();
        }
        if (collision.gameObject.tag.Equals("Terrain") && collision.contacts[0].point.y == collision.contacts[1].point.y)
        {
            lockUp = false;
            lockLeft = false;
            lockRight = false;
        }
        if (collision.gameObject.tag.Equals("Terrain") && collision.contacts[0].point.y != collision.contacts[1].point.y)
        {
            
            if (collision.contacts[0].point.x < player.position.x)
            {
                lockLeft = true;
            }
            if (collision.contacts[1].point.x > player.position.x)
            {
                lockRight = true;
            }
            lockUp = true;
        }
    }

    void Death()
    {
        //GUI Transition
        SceneManager.LoadScene("BasicScene1");
    }
}

