using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    bool harmfulToMonsters = false;
    private float DestroyTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        DestroyTime = Time.time + 10f;
    }

    private void FixedUpdate()
    {
        if (DestroyTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }

    public void Return()
    {
        rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
        harmfulToMonsters = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Avatar"))
        {
            collision.gameObject.GetComponent<playercontroller>().TakeDamage(transform);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Equals("Enemy") && harmfulToMonsters)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(10);
            if(collision.gameObject.GetComponent<TemperedGlass>() != null)
            {
                collision.gameObject.GetComponent<TemperedGlass>().Incapacitate();
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Equals("Enemy") && !harmfulToMonsters)
        {
            
        }
        else
        {
            
        }
    }
}
