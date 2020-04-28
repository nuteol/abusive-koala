using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackingscript : MonoBehaviour
{
    [SerializeField] private LayerMask Paper;
    [SerializeField] private LayerMask Glass;
    [SerializeField] private LayerMask Metal;
    private Transform attackPos;


    private abstract class weapon
    {
        public string name;
        public float nextAttackTime = 0f;
        public int damagePaper;
        public int damageGlass;
        public int damageMetal;
        public float attackRange;
        public abstract void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem);
    }
    private class hand : weapon
    {
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem)
        {
            
        }
    }
    private class cardboardCutter : weapon
    {
        public cardboardCutter()
        {
            name = "Cardboard Cutter";
            damagePaper = 5;
            damageGlass = 1;
            damageMetal = 0;
            attackRange = 1.5f;
        }
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem)
        {
            if(nextAttackTime <= Time.time)
            {
                SoundManager.PlaySound("playerHit");
                Collider2D[] paperEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wiep);
                foreach (Collider2D enemy in paperEnemiesToDamage)
                {
                    if(enemy.GetComponent<Enemy>() != null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damagePaper);
                    }
                    else if(enemy.GetComponent<Roller>() != null)
                    {
                        enemy.GetComponent<Roller>().TakeDamage(damagePaper);
                    }
                }
                Collider2D[] glassEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wieg);
                foreach (Collider2D enemy in glassEnemiesToDamage)
                {
                    ; enemy.GetComponent<Enemy>().TakeDamage(damageGlass);
                }
                Collider2D[] metalEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wiem);
                foreach (Collider2D enemy in metalEnemiesToDamage)
                {
                    ; enemy.GetComponent<Enemy>().TakeDamage(damageMetal);
                }
                nextAttackTime = Time.time + 1f / 2;
            }
        }
    }
    weapon currentWeapon = new hand();
    weapon cardboardcutter;
    
    // Start is called before the first frame update
    void Start()
    {
        attackPos = GetComponent<Transform>();
        cardboardcutter = new cardboardCutter();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1) && currentWeapon != cardboardcutter)
        {
            currentWeapon = cardboardcutter;
            SoundManager.PlaySound("playerDraw");
        }
        if (Input.GetKey(KeyCode.X))
        {
            currentWeapon.weaponAttack(attackPos, Paper, Glass, Metal);
        }
    }
}
