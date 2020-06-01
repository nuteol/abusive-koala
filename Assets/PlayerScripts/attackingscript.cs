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
    public Animator animator;
    private Transform attackPos;
    private float nextDrawTime = 0f;
    public int[] unlockedWeapons;

    
    private abstract class weapon
    {
        public Animator animator2;
        public string name;
        public float nextAttackTime = 0f;
        public int damagePaper;
        public int damageGlass;
        public int damageMetal;
        public float attackRange;
        public float attackTime;
        public abstract void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles, Animator animator);
        public abstract void draw();
        public abstract void playSound();
    }
    private class hand : weapon
    {
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles, Animator animator)
        {
            
        }
        public override void draw()
        {
            
        }
        public override void playSound()
        {

        }
    }
    private class cardboardCutter : weapon
    {
        public cardboardCutter()
        {
            name = "Cardboard Cutter";
            damagePaper = 20;
            damageGlass = 1;
            damageMetal = 0;
            attackRange = 1.4f;
            attackTime = 0.5f;
        }
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles, Animator animator)
        {
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
        }
        public override void draw()
        {
            SoundManager.PlaySound("playerDraw");
        }
        public override void playSound()
        {
            SoundManager.PlaySound("playerHit");
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
            attackRange = 1.4f;
            attackTime = 1.3f;
        }
        public override void weaponAttack(Transform ap, LayerMask wiep, LayerMask wieg, LayerMask wiem, LayerMask projectiles, Animator animator)
        {
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
        }
        public override void draw()
        {
            SoundManager.PlaySound("maceDraw");
        }
        public override void playSound()
        {
            SoundManager.audrioSrc.volume = 6f;
            SoundManager.PlaySound("maceHit");
            SoundManager.audrioSrc.volume = 1f;
        }
    }
    weapon currentWeapon;
    hand Hand = new hand();
    weapon[] weapons = new weapon[4];

    IEnumerator ExecuteAttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);        
        currentWeapon.weaponAttack(attackPos, Paper, Glass, Metal, Projectile, animator);
        //animator.SetTrigger("Weapon1Attack");
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

        for(int i = 0; i < unlockedWeapons.Length; i++)
        {
            if(unlockedWeapons[i] == 1)
            {
                UnlockWeapon1();
            }
            if (unlockedWeapons[i] == 2)
            {
                UnlockWeapon2();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1) && currentWeapon.name != "Cardboard Cutter" && nextDrawTime <= Time.time && weapons[1] != Hand)
        {
            currentWeapon = new cardboardCutter();
            currentWeapon.draw();
            animator.SetBool("Weapon1Equipped", true);
            animator.SetBool("Weapon2Equipped", false);
        }
        if (Input.GetKey(KeyCode.Alpha2) && currentWeapon.name != "Metal Mace" && nextDrawTime <= Time.time && weapons[2] != Hand)
        {
            currentWeapon = new metalMace();
            currentWeapon.draw();
            animator.SetBool("Weapon2Equipped", true);
            animator.SetBool("Weapon1Equipped", false);
        }
        if (Input.GetKey(KeyCode.X))
        {
            if(currentWeapon.nextAttackTime <= Time.time)
            {
                if (currentWeapon != Hand)
                {
                    nextDrawTime = Time.time + 0.5f;
                }
                //Ssound plays here
                currentWeapon.playSound();
                //Do animation here
                animator.SetTrigger("Weapon1Attack");

                StartCoroutine(ExecuteAttackAfterTime(0.4f));
                currentWeapon.nextAttackTime = Time.time + currentWeapon.attackTime;
            }
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
