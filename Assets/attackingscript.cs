using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackingscript : MonoBehaviour
{
    private float nextAttackTime = 0f;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.X))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                foreach (Collider2D enemy in enemiesToDamage)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
                nextAttackTime = Time.time + 1f;
            }
        }
    }
}
