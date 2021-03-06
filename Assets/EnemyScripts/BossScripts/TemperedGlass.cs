﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemperedGlass : Enemy
{
    private enum states { intro, idle, spinning, throwing, walking, incapacitated, gettingUp, dead };

    private string BossName = "Tempered Glass";
    public TextMeshProUGUI name;
    private float maxHealth = 150;
    private float currentHealth;
    public GameObject healthBarGameObject;
    public Image healthBar;
    public MoveToNextLevel state;
    public GameObject exit;

    private bool facingRight = false;
    private float xSpeed = -2;
    private float xSpinSpeed = -4;
    private states currentState;

    private float idleTime = 2;
    private float walkTime = 3;
    private float spinTime = 0.5f;
    private float throwTime = 1;
    private float incapacitatedTime = 4;
    private float getUpTime = 2;
    private float currentSwitchTime;

    private BoxCollider2D bosscollider;
    private Rigidbody2D rb;
    private Transform glassT;
    public Animator animator;

    private Transform target;
    
    private float distance;
    private float altitude;
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        healthBarGameObject.SetActive(false);
        exit.SetActive(false);
        currentState = states.idle;
        name.text = BossName;
        currentSwitchTime = Time.time + idleTime;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        glassT = GetComponent<Transform>();
        bosscollider = GetComponent<BoxCollider2D>();
        target = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        if (currentState == states.walking)
        {
            if (distance * distance < 1)
            {
                currentSwitchTime = Time.time;
            }
        }
        altitude = target.position.y - transform.position.y + 0.5f;
        distance = target.position.x - transform.position.x;
        if (facingRight && target.position.x - transform.position.x < 0 && currentState != states.incapacitated && currentState != states.gettingUp)
        {
            Flip();
        }
        else if (!facingRight && target.position.x - transform.position.x > 0 && currentState != states.incapacitated && currentState != states.gettingUp)
        {
            Flip();
        }
        if (currentSwitchTime <= Time.time)
        {
            switchState();
        }
        if(Mathf.Sqrt(distance * distance/2 + altitude + altitude) < 2)
        {
            if(currentState != states.incapacitated && currentState != states.gettingUp)
            {
                animator.SetBool("walking", false);
                animator.SetBool("throw", false);
                animator.SetBool("spinning", true);
                currentState = states.spinning;
                rb.velocity = new Vector2(xSpinSpeed, 0);
                currentSwitchTime = Time.time + spinTime;
            }
        }
    }

    void switchState()
    {
        if(currentState == states.idle)
        {
            currentState = states.walking;
            animator.SetBool("walking", true);
            currentSwitchTime = walkTime + Time.time;
            rb.velocity = new Vector2(xSpeed, 0);
        }
        else if(currentState == states.walking)
        {
            currentState = states.throwing;
            animator.SetBool("walking", false);
            animator.SetBool("throw", true);
            currentSwitchTime = throwTime + Time.time;
            rb.velocity = new Vector2(0, 0);
        }
        else if(currentState == states.throwing)
        {
            currentState = states.idle;
            animator.SetBool("throw", false);
            currentSwitchTime = idleTime + Time.time;
            Shoot();
        }
        else if(currentState == states.spinning)
        {
            currentState = states.idle;
            animator.SetBool("spinning", false);
            currentSwitchTime = idleTime + Time.time;
            rb.velocity = new Vector2(0, 0);
        }
        else if(currentState == states.incapacitated)
        {
            currentState = states.gettingUp;
            animator.SetBool("dazzed", false);
            animator.SetBool("wake", true);
            currentSwitchTime = getUpTime + Time.time;
        }
        else if (currentState == states.gettingUp)
        {
            currentState = states.idle;
            animator.SetBool("wake", false);
            currentSwitchTime = idleTime + Time.time;
        }
    }

    void Flip()
    {
        if(rb.velocity.x != 0 && currentState != states.spinning)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            
        }
        facingRight = !facingRight;
        xSpeed = -xSpeed;
        xSpinSpeed = -xSpinSpeed;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        return;
    }

    void Shoot()
    {
        //spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        animator.SetBool("throw", false);
        bulletRb.velocity = new Vector2(5*distance / Mathf.Sqrt(altitude * altitude + distance * distance), 5*altitude / Mathf.Sqrt(altitude * altitude + distance * distance));
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentState == states.spinning)
        {
            if (collision.gameObject.tag.Equals("Avatar"))
            {
                collision.gameObject.GetComponent<playercontroller>().TakeDamage(glassT);
            }
        }
    }

    public override bool TakeDamage(int damage)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //MoveToNextLevel.End();
            Death();
        }
        return (currentHealth <= 0);
    }

    public void Incapacitate()
    {
        animator.SetBool("walking", false);
        animator.SetBool("throw", false);
        animator.SetTrigger("fall");
        animator.SetBool("dazzed", true);
        currentSwitchTime = Time.time + incapacitatedTime;
        currentState = states.incapacitated;
        rb.velocity = new Vector2(0, 0);
    }

    public override void Death()
    {
        healthBarGameObject.SetActive(false);
        SoundManager.PlaySound("glassCanonDeath");
        SoundManager.audrioSrc.Stop();
        state.isDead = true;
        Destroy(gameObject);
        exit.SetActive(true);
        
    }
}
