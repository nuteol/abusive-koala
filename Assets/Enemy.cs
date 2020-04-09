﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth = 10;
    private int currentHealth;

    private bool moveRight = true;
    private Rigidbody2D rb;
    private Transform enemyT;
    public float moveSpace;
    private float patrolCenter;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyT = GetComponent<Transform>();
        patrolCenter = enemyT.position.x;
    }

    private void Update()
    {
        if(moveRight)
        {
            rb.velocity = new Vector2(Random.Range(0, 2), 0);
        }
        else
        {
            rb.velocity = new Vector2(-Random.Range(0, 2), 0);
        }
        if (enemyT.position.x > patrolCenter + moveSpace/2)
        {
            moveRight = false;
        }
        if (enemyT.position.x < patrolCenter - moveSpace / 2)
        {
            moveRight = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Avatar"))
        {
            print("Lmao");
            collision.gameObject.GetComponent<playercontroller>().TakeDamage(enemyT);
        }
    }

    void Death()
    {
        //Death anime here
        //End death anim
        Destroy(gameObject);
    }
}
