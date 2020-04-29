using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    private enum stages { full, halfFull, empty };
    private enum states { intro, idle, chargingF, chargingB, fastRoll, bounceRoll, dead };

    

    private float mahHealth = 300;
    private float currentHealth;
    private stages currentStage = stages.full;
    private float idleTime = 8;
    private float chargeTime = 3;
    private float rollTime = 1;
    private float currentSwitchTime;

    private bool facingRight = false;
    private float xSpeed = -20;
    private states currentState;

    private Rigidbody2D rb;
    private Transform rollerT;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        //Play Animation currentstate is intro/Wait to finish
        currentState = states.idle;
        currentSwitchTime = Time.time + idleTime;
        currentHealth = mahHealth;
        rb = GetComponent<Rigidbody2D>();
        rollerT = GetComponent<Transform>();
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
                currentState = states.chargingF;
            }
            else
            {
                currentState = states.chargingB;
            }
            currentSwitchTime = chargeTime + Time.time;
            
        }
        else if (currentState == states.chargingF)
        {
            currentSwitchTime = rollTime + Time.time;
            currentState = states.fastRoll;            
            rb.velocity = new Vector2(xSpeed, 0);
            animator.SetBool("isRolling", true);
        }
        else if (currentState == states.chargingB)
        {
            currentSwitchTime = rollTime * 4 + Time.time;
            currentState = states.bounceRoll;
            rb.velocity = new Vector2(xSpeed/4, 20);
            rb.bodyType = RigidbodyType2D.Dynamic;
            animator.SetBool("isJumping", true);
        }
        else if (currentState == states.fastRoll || currentState == states.bounceRoll)
        {
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
        if(collision.gameObject.tag == "RollerFloor" && currentState == states.bounceRoll)
        {
            rb.velocity = new Vector2(xSpeed / 4, 20);
        }
        else if(collision.gameObject.tag == "RollerFloor" && currentState == states.idle)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2(0, 0);
            rollerT.position = new Vector3(rollerT.position.x, 4, rollerT.position.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(currentState == states.fastRoll || currentState == states.bounceRoll)
        {
            if(collision.gameObject.tag.Equals("Avatar"))
            {
                collision.gameObject.GetComponent<playercontroller>().TakeDamage(rollerT);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthBar.fillAmount = currentHealth / maxHealth;
        if(currentHealth <= 200 && currentHealth > 100 && currentStage == stages.full)
        {
            currentStage = stages.halfFull;
        }
        else if(currentHealth <= 100 && currentHealth > 0 && currentStage == stages.halfFull)
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
        /*if (spec.GetSpecAmount() + damage < 100)
        {
            spec.AddSpecial(damage);
        }
        else
        {
            spec.SetSpecial(100);
        }*/
    }

    void Death()
    {
        //Death anime here
        //End death anim
        Destroy(gameObject);
        SoundManager.PlaySound("monsterDeath");
    }
}
