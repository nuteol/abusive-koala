using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Roller : Enemy
{
    private enum stages { full, halfFull, empty };
    private enum states { intro, idle, chargingF, chargingB, fastRoll, bounceRoll, dead };

    private string BossName = "Toilet Roller";
    public TextMeshProUGUI name;
    public Image endScreen;
    public TextMeshProUGUI endText;
    public MoveToNextLevel state;
    public GameObject exit;

    private float mahHealth = 300;
    private float currentHealth;
    public Image healthBar;
    public GameObject healthBarGameObject;

    private stages currentStage = stages.full;
    private float idleTime = 8;
    private float chargeTime = 3;
    private float rollTime = 1;
    private float currentSwitchTime;

    private bool facingRight = false;
    private float xSpeed = -20;
    private states currentState;

    private BoxCollider2D bosscollider;
    private Rigidbody2D rb;
    private Transform rollerT;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        exit.SetActive(false);
        endScreen.enabled = false;
        endText.enabled = false;
        name.text = BossName;
        healthBarGameObject.SetActive(false);
        //Play Animation currentstate is intro/Wait to finish
        currentState = states.idle;
        currentSwitchTime = Time.time + idleTime;
        currentHealth = mahHealth;
        rb = GetComponent<Rigidbody2D>();
        rollerT = GetComponent<Transform>();
        bosscollider = GetComponent<BoxCollider2D>();
    }


    // Changed to fixed update for accuracy
    void FixedUpdate()
    {
        if (currentSwitchTime <= Time.time)
        {
            switchState();
        }
    }

    void switchState()
    {
        if (currentState == states.idle)
        {
            if (Random.Range(0, 2) == 1)
            {
                bosscollider.offset = new Vector2(0, -0.9f);
                bosscollider.size = new Vector2(3.04f, 3);
                currentState = states.chargingF;
                animator.SetBool("isChargingF", true);
            }
            else
            {
                currentState = states.chargingB;
                animator.SetBool("isChargingB", true);
            }
            currentSwitchTime = chargeTime + Time.time;
            
        }
        else if (currentState == states.chargingF)
        {
            animator.SetBool("isChargingF", false);
            currentSwitchTime = rollTime + Time.time;
            currentState = states.fastRoll;            
            rb.velocity = new Vector2(xSpeed, 0);
            animator.SetBool("isRolling", true);
        }
        else if (currentState == states.chargingB)
        {
            bosscollider.offset = new Vector2(0, -0.9f);
            bosscollider.size = new Vector2(3.04f, 3);
            animator.SetBool("isChargingB", false);
            currentSwitchTime = rollTime * 4 + Time.time;
            currentState = states.bounceRoll;
            rb.velocity = new Vector2(xSpeed/4, 20);
            rb.bodyType = RigidbodyType2D.Dynamic;
            animator.SetBool("isRolling", true);
        }
        else if (currentState == states.fastRoll || currentState == states.bounceRoll)
        {
            bosscollider.offset = new Vector2(0, -0.3f);
            bosscollider.size = new Vector2(3.04f, 4.38f);
            currentSwitchTime = idleTime + Time.time;
            currentState = states.idle;            
            rb.velocity = new Vector2(0, 0);
            Flip();
            animator.SetBool("isRolling", false);
            animator.SetBool("isJumping", false);
        }
    }

    //Set positions because slight changes
    void Flip()
    {
        facingRight = !facingRight;
        xSpeed = -xSpeed;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RollerFloor" && currentState == states.bounceRoll)
        {
            rb.velocity = new Vector2(xSpeed / 4, 20);
        }
        else if(collision.gameObject.tag == "RollerFloor" && currentState == states.idle)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2(0, 0);
            rollerT.position = new Vector3(rollerT.position.x, 3.5f, rollerT.position.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentState == states.fastRoll || currentState == states.bounceRoll)
        {
            if(collision.gameObject.tag.Equals("Avatar"))
            {
                collision.gameObject.GetComponent<playercontroller>().TakeDamage(rollerT);
            }
        }
    }

    public override bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / mahHealth;
        if (currentHealth <= 200 && currentHealth > 100 && currentStage == stages.full)
        {
            currentStage = stages.halfFull;
        }
        else if (currentHealth <= 100 && currentHealth > 0 && currentStage == stages.halfFull)
        {
            currentStage = stages.empty;
            //Boss moves faster
            idleTime = idleTime / 2;
            chargeTime = chargeTime / 2;
            xSpeed = xSpeed * 2;
            rollTime = rollTime / 2;
        }
        if (currentHealth <= 0)
        {
            currentState = states.dead;
            Death();
            
        }
        return (currentHealth <= 0);
        /*if (spec.GetSpecAmount() + damage < 100)
        {
            spec.AddSpecial(damage);
        }
        else
        {
            spec.SetSpecial(100);
        }*/
    }

    public override void Death()
    {
        //Death anime here
        //End death anim
        healthBarGameObject.SetActive(false);
        SoundManager.audrioSrc.Stop();
        Destroy(gameObject);
        SoundManager.PlaySound("monsterDeath");
        Debug.Log("BossDead");
        state.isDead = true;
        exit.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); // next level load
        //endScreen.enabled = true;
        //endText.enabled = true;
    }
}
