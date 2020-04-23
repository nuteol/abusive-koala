using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;
    public HealthBar hp;
    public SpecialAttack spec;

    private bool isCoroutineExecuting = false;
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    private Transform playerT;
    public KeyCode equip1, attack1;

    public float runSpeed;
    public float jumpForce;
    private float horizontalMove = 0f;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkradius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;

    private float damagedtime;
    private bool takingDamage;
    private bool InputEnabled = true;


    private void Start()
    {
        extraJumps = extraJumpValue;

        spec.SetMaxSpecial(100);
        spec.SetSpecial(0);
        currentHearts = maxHearts;
        hp.SetMaxHealth(maxHearts);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        playerT = GetComponent<Transform>();
        gameObject.SetActive(true);
        damagedtime = Time.time;
        rb.isKinematic = false;
    }

    private void Update()
    {
        //animator.SetBool("IsJumping", rb.velocity.y != 0);
        takingDamage = (damagedtime >= Time.time);

        if(isGrounded)
        {
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
                SoundManager.PlaySound("playerJump");
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("IsJumping", true);
            }
            if(rb.velocity.y < -5 && !isGrounded)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
            }
            else if(rb.velocity.y == 0)
            {
                animator.SetBool("IsFalling", false);
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

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkradius, whatIsGround);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Death"))
        {
            //Play animation or something
            SoundManager.PlaySound("playerDeath");
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            StartCoroutine(ExecuteDeathAfterTime(1));
        }
    }

    public void TakeDamage(Transform enemyT)
    {
        currentHearts--;
        hp.SetHealth(currentHearts);
        damagedtime = Time.time + 0.4f;
        takingDamage = true;
        SoundManager.PlaySound("playerGetHit"); 
        if (enemyT.position.x < playerT.position.x)
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
            SoundManager.PlaySound("playerDeath");
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            StartCoroutine(ExecuteDeathAfterTime(1));
           
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
        SceneManager.LoadScene("BasicScene1");

    }
}

