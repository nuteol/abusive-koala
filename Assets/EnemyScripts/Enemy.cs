using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float maxHealth = 10;
    private float currentHealth;
    public Image healthBar;
    public SpecialAttack spec;

    private bool moveRight = true;
    private Rigidbody2D rb;
    private Transform enemyT;
    public float moveSpace;
    private float patrolCenter;
    public Animator animator;

    public GameObject lootDrop;

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
            animator.SetFloat("Speed", 1);
        }
        else
        {
            rb.velocity = new Vector2(-Random.Range(0, 2), 0);
            animator.SetFloat("Speed", 1);
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

    public bool TakeDamage(int damage)
    {
        spec.AddSpecial(damage);
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Death();
        }
        return (currentHealth <= 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Avatar"))
        {
            collision.gameObject.GetComponent<playercontroller>().TakeDamage(enemyT);
        }
    }


    void Death()
    {
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.PlaySound("monsterDeath");
    }
}
