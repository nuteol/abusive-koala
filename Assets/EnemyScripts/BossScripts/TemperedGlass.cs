using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperedGlass : Enemy
{
    private enum states { intro, idle, spinning, throwing, walking, incapacitated, gettingUp, dead };

    private string BossName = "Tempered Glass";
    private float maxHealth = 500;
    private float currentHealth;

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

    private Transform target;
    
    private float distance;
    private float altitude;
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.idle;
        currentSwitchTime = Time.time + idleTime;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        glassT = GetComponent<Transform>();
        bosscollider = GetComponent<BoxCollider2D>();
        target = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        altitude = target.position.y - transform.position.y + 0.5f;
        distance = target.position.x - transform.position.x;
        if (facingRight && target.position.x - transform.position.x < 0)
        {
            Flip();
        }
        else if (!facingRight && target.position.x - transform.position.x > 0)
        {
            Flip();
        }
        if (currentSwitchTime <= Time.time && Mathf.Sqrt(distance * distance/2 + altitude + altitude) >= 3)
        {
            switchState();
        }
        if(Mathf.Sqrt(distance * distance/2 + altitude + altitude) < 3)
        {
            if(currentState != states.incapacitated && currentState != states.gettingUp)
            {
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
            currentSwitchTime = walkTime + Time.time;
            rb.velocity = new Vector2(xSpeed, 0);
        }
        else if(currentState == states.walking)
        {
            currentState = states.throwing;
            currentSwitchTime = throwTime + Time.time;
            rb.velocity = new Vector2(0, 0);
        }
        else if(currentState == states.throwing)
        {
            currentState = states.idle;
            currentSwitchTime = idleTime + Time.time;
            Shoot();
        }
        else if(currentState == states.spinning)
        {
            currentState = states.idle;
            currentSwitchTime = idleTime + Time.time;
            rb.velocity = new Vector2(0, 0);
        }
        else if(currentState == states.incapacitated)
        {
            currentState = states.gettingUp;
            currentSwitchTime = getUpTime + Time.time;
        }
        else if (currentState == states.gettingUp)
        {
            currentState = states.idle;
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
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            
            Death();

        }
        return (currentHealth <= 0);
    }

    public void Incapacitate()
    {
        currentSwitchTime = Time.time + incapacitatedTime;
        currentState = states.incapacitated;
        rb.velocity = new Vector2(0, 0);
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
