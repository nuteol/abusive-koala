﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;
    public HealthBar hp;
    public static playercontroller instance;

    private bool isCoroutineExecuting = false;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject youDied;
    public GameObject hpBar;
    private Collider2D coll;
    private Transform playerT;
    public KeyCode equip1, attack1;

    public float runSpeed;
    public float jumpForce;
    private float horizontalMove = 0f;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheckR;
    public Transform groundCheckL;
    public float checkradius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;

    private float damagedtime;
    private bool takingDamage;
    private bool InputEnabled = true;

    public float nextDamageTime = 0f;

    public string currentScene = "";

    private void Start()
    {
        extraJumps = extraJumpValue;
        currentHearts = maxHearts;
        hp.SetMaxHealth(maxHearts);
        if (instance == null)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        playerT = GetComponent<Transform>();
        gameObject.SetActive(true);
        damagedtime = Time.time;
        rb.isKinematic = false;
        youDied.SetActive(false);
        hpBar.SetActive(true);
    }

    private void Update()
    {
        //animator.SetBool("IsJumping", rb.velocity.y != 0);
        takingDamage = (damagedtime >= Time.time);

        if(isGrounded)
        {
            //animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
            extraJumps = extraJumpValue;
        }
        
            if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
                animator.SetBool("IsJumping", true);
                SoundManager.PlaySound("playerJump");
        }
            else if(Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("IsJumping", true);
                SoundManager.PlaySound("playerJump");
        }
            if(rb.velocity.y < -5 && !isGrounded)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
            }
            else if(rb.velocity.y == 0)
            {
                animator.SetBool("IsFalling", false);
                animator.SetBool("IsJumping", false);
            }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckR.position, checkradius, whatIsGround) || Physics2D.OverlapCircle(groundCheckL.position, checkradius, whatIsGround) || Physics2D.OverlapCircle(groundCheckR.position, checkradius, LayerMask.GetMask("Glass")) || Physics2D.OverlapCircle(groundCheckL.position, checkradius, LayerMask.GetMask("Glass")) || Physics2D.OverlapCircle(groundCheckR.position, checkradius, LayerMask.GetMask("Paper")) || Physics2D.OverlapCircle(groundCheckL.position, checkradius, LayerMask.GetMask("Paper"));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        if(facingRight == false && horizontalMove > 0)
        {
            Flip();
        }
        else if(facingRight && horizontalMove < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public void ExecuteDeath()
    {
        SoundManager.PlaySound("playerDeath");
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        youDied.SetActive(true);
        PlayerPrefs.SetInt("Coins", 0);
        //SoundManager.PlaySound("playerYouDied");
        StartCoroutine(ExecuteDeathAfterTime(1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Death"))
        {
            //Play animation or something

            animator.SetBool("dead", true);
            ExecuteDeath();
        }
    }
    public void addHp(int value)
    {
        if(currentHearts + value > maxHearts)
        {
            print("fullhp");
        }
        else
        {
            currentHearts += value;
            hp.SetHealth(currentHearts);
        }
        
    }
    public void TakeDamage(Transform enemyT)
    {
        if(nextDamageTime <= Time.time)
        {
            nextDamageTime = Time.time + 2;
            currentHearts--;
            hp.SetHealth(currentHearts);
            damagedtime = Time.time + 0.4f;
            takingDamage = true;
            SoundManager.PlaySound("playerGetHit");
            if (enemyT.position.x < playerT.position.x)
            {
                rb.velocity = new Vector2(5 * (playerT.position.x - enemyT.position.x), 10);
            }
            else
            {
                if (enemyT.position.x > playerT.position.x)
                {
                    rb.velocity = new Vector2(-5 * (enemyT.position.x - playerT.position.x), 10);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 10);
                }
            }

            if (currentHearts <= 0)
            {
                //Play animation or something
                animator.SetBool("dead", true);
                ExecuteDeath();

            }
        }
        
    }
    IEnumerator ExecuteDeathAfterTime(float time)
    {
        
        yield return new WaitForSeconds(time);
        Death();
    }
    
    private void Death()
    {
        //GUI Transition
        PlayerPrefs.SetInt("Coins", 0);
        SceneManager.LoadScene(currentScene);
    }
}

