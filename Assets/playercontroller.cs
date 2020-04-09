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
    private Transform playerT;
    public KeyCode equip1, attack1;

    public float runSpeed = 20f;
    public float jumpForce = 20f;
    float horizontalMove = 0f;

    private bool lockUp = false;
    private bool lockDown = false;
    private bool lockLeft = false;
    private bool lockRight = false;

    private float damagedtime;
    private bool takingDamage;

    //[SerializeField] private LayerMask ground;
    //[SerializeField] private LayerMask wall;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        playerT = GetComponent<Transform>();

        damagedtime = Time.time;
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetBool("IsJumping", rb.velocity.y != 0);
        takingDamage = (damagedtime >= Time.time);
        if (Input.GetKey(KeyCode.W) && rb.velocity.y == 0 && !lockUp && !takingDamage)//coll.IsTouchingLayers(ground) //rb.velocity.y <= 1.192094e-07 && rb.velocity.y >= -1.192094e-07
        {
            lockUp = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetKey(KeyCode.A) && !lockLeft && !takingDamage)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            lockRight = false;
        }
        else
        {
            if (!Input.GetKey(KeyCode.D) && !takingDamage)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.D) && !lockRight && !takingDamage)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            lockLeft = false;
        }
        else
        {
            if(!Input.GetKey(KeyCode.A) && !takingDamage)
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
            //Play animation or something
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
            
            if (collision.contacts[0].point.x < playerT.position.x)
            {
                lockLeft = true;
            }
            if (collision.contacts[1].point.x > playerT.position.x)
            {
                lockRight = true;
            }
            lockUp = true;
        }
    }

    public void TakeDamage(Transform enemyT)
    {
        currentHearts--;
        damagedtime = Time.time + 0.4f;
        takingDamage = true;
        if(enemyT.position.x < playerT.position.x)
        {
            rb.velocity = new Vector2(5*(playerT.position.x - enemyT.position.x), 10);
        }
        else
        {
            if (enemyT.position.x > playerT.position.x)
            {
                rb.velocity = new Vector2(-5 *(enemyT.position.x - playerT.position.x), 10);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 10);
            }
        }
        
        if(currentHearts <= 0)
        {
            //Play animation or something
            Death();
        }
    }

    private void Death()
    {
        //GUI Transition
        SceneManager.LoadScene("BasicScene1");
    }
}

