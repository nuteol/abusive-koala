using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardboardCunt : Enemy
{
    private float maxHealth = 10;
    private float currentHealth;
    public Image healthBar;

    private bool moveRight = true;
    private Rigidbody2D rb;
    private Transform enemyT;
    public float moveSpace;
    private float patrolCenter;
    public Animator animator;

    public GameObject coinDrop;
    public GameObject potionDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyT = GetComponent<Transform>();
        patrolCenter = enemyT.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            rb.velocity = new Vector2(Random.Range(0, 2), 0);
            animator.SetFloat("Speed", 1);
        }
        else
        {
            rb.velocity = new Vector2(-Random.Range(0, 2), 0);
            animator.SetFloat("Speed", 1);
        }
        if (enemyT.position.x > patrolCenter + moveSpace / 2 && moveRight)
        {
            moveRight = false;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            enemyT.localScale = Scaler;

            Vector3 HpScaler = healthBar.gameObject.transform.localScale;
            HpScaler.x *= -1;
            healthBar.gameObject.transform.localScale = HpScaler;
        }
        else if (enemyT.position.x < patrolCenter - moveSpace / 2 && !moveRight)
        {
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            enemyT.localScale = Scaler;

            Vector3 HpScaler = healthBar.gameObject.transform.localScale;
            HpScaler.x *= -1;
            healthBar.gameObject.transform.localScale = HpScaler;

            moveRight = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Avatar"))
        {
            collision.gameObject.GetComponent<playercontroller>().TakeDamage(enemyT);
        }
    }

    public override bool TakeDamage(int damage)
    {
        {
            currentHealth -= damage;
            healthBar.fillAmount = currentHealth / maxHealth;
            if (currentHealth <= 0)
            {
                Death();
            }
            return (currentHealth <= 0);
        }
    }

    public override void Death()
    {

        //if (Random.Range(0, 1) == 1)
        //{
            Instantiate(coinDrop, transform.position, Quaternion.identity);
        //}

        //else
        //{
            //Instantiate(potionDrop, transform.position, Quaternion.identity);
        //}

        Destroy(gameObject);
        SoundManager.PlaySound("monsterDeath");
    }
}
