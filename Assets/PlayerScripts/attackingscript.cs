using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attackingscript : MonoBehaviour
{
    [SerializeField] private LayerMask Paper;
    [SerializeField] private LayerMask Glass;
    [SerializeField] private LayerMask Metal;
    [SerializeField] private LayerMask Projectile;
    private Transform attackPos;
    private float nextDrawTime = 0f;

    private abstract class weapon
    {
        public string name;
        public float nextAttackTime = 0f;
        public int damagePaper;
        public int damageGlass;
        public int damageMetal;
        public float attackRange;
        public abstract void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles);
        public abstract void draw();
    }
    private class hand : weapon
    {
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles)
        {
            
        }
        public override void draw()
        {
            
        }
    }
    private class cardboardCutter : weapon
    {
        public cardboardCutter()
        {
            name = "Cardboard Cutter";
            damagePaper = 50;
            damageGlass = 1;
            damageMetal = 0;
            attackRange = 1.3f;
        }
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles)
        {
            if(nextAttackTime <= Time.time)
            {
                SoundManager.PlaySound("playerHit");
                //Do animation here
                Collider2D[] paperEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wiep);
                foreach (Collider2D enemy in paperEnemiesToDamage)
                {
                    if (enemy.GetComponent<Enemy>() != null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damagePaper);
                    }
                }
                Collider2D[] glassEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wieg);
                foreach (Collider2D enemy in glassEnemiesToDamage)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damageGlass);
                }
                nextAttackTime = Time.time + 1f / 2;
            }
        }
        public override void draw()
        {
            SoundManager.PlaySound("playerDraw");
        }
    }
    private class metalMace : weapon
    {
        public metalMace()
        {
            name = "Metal Mace";
            damagePaper = 1;
            damageGlass = 10;
            damageMetal = 2;
            attackRange = 1.5f;
        }
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles)
        {
            if (nextAttackTime <= Time.time)
            {
                SoundManager.PlaySound("playerHit");
                //Do animation here
                Collider2D[] paperEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wiep);
                foreach (Collider2D enemy in paperEnemiesToDamage)
                {
                    if (enemy.GetComponent<Enemy>() != null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damagePaper);
                    }
                }
                Collider2D[] glassEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wieg);
                foreach (Collider2D enemy in glassEnemiesToDamage)
                {
                    if (enemy.GetComponent<Enemy>() != null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(damageGlass);
                    }
                }
                Collider2D[] ProjectilesToHit = Physics2D.OverlapCircleAll(ap.position, attackRange, projectiles);
                foreach (Collider2D projectile in ProjectilesToHit)
                {
                    if (projectile.GetComponent<Projectile>() != null)
                    {
                        projectile.GetComponent<Projectile>().Return();
                    }
                }
                nextAttackTime = Time.time + 2f;
            }
        }
        public override void draw()
        {
            SoundManager.PlaySound("playerDraw");
        }
    }
    weapon currentWeapon;
    hand Hand = new hand();
    weapon[] weapons = new weapon[4];

    IEnumerator ExecuteAttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        currentWeapon.weaponAttack(attackPos, Paper, Glass, Metal, Projectile);
    }

    // Start is called before the first frame update
    void Start()
    {
        attackPos = GetComponent<Transform>();
        weapons[0] = Hand;
        weapons[1] = Hand;
        weapons[2] = Hand;
        weapons[3] = Hand;
        currentWeapon = weapons[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1) && currentWeapon != weapons[1] && nextDrawTime <= Time.time && weapons[1] != Hand)
        {
            currentWeapon = weapons[1];
            currentWeapon.draw();
        }
        if (Input.GetKey(KeyCode.Alpha2) && currentWeapon != weapons[2] && nextDrawTime <= Time.time && weapons[2] != Hand)
        {
            currentWeapon = weapons[2];
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (currentWeapon != Hand)
            {
                nextDrawTime = Time.time + 2f;
            }
            StartCoroutine(ExecuteAttackAfterTime(0.4f));
        }
    }

    public void UnlockWeapon1()
    {
        weapons[1] = new cardboardCutter();
    }
    public void UnlockWeapon2()
    {
        weapons[2] = new metalMace();
    }
}
