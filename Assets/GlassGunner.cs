using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassGunner : Enemy
{
    private float maxHealth = 10;
    private float currentHealth;
   public Image healthBar;

    private bool facingRight = false;
    private Rigidbody2D rb;
    private Transform enemyT;
    //public Animator animator;

    public GameObject lootDrop;

    private Transform player;

    private float nextShotTime;
    private float distance;
    private float altitude;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyT = GetComponent<Transform>();
        player = GameObject.Find("Player").transform;
    }
    
    void FixedUpdate()
    {
        altitude = player.position.y - transform.position.y;
        distance = player.position.x - transform.position.x;
        if(facingRight && player.position.x - transform.position.x < 0)
        {
            Flip();
        }
        else if(!facingRight && player.position.x - transform.position.x > 0)
        {
            Flip();
        }

        if(distance*distance < 100 && nextShotTime <= Time.time && altitude/distance < 2)
        {
            Shoot();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
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
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.PlaySound("glassCanonDeath"); 
    }

    void Shoot()
    {
        nextShotTime = Time.time + 4f;
        //spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if(facingRight)
        {
            bulletRb.velocity = new Vector2(6, 0);
        }
        else
        {
            bulletRb.velocity = new Vector2(-6, 0);
        }
    }
}
