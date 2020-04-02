using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackingscript : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemies;
    //[SerializeField] private LayerMask Cardboards;
    //[SerializeField] private LayerMask Glass;
    //[SerializeField] private LayerMask Metal;
    private Transform attackPos;
    
    
    private abstract class weapon
    {
        public string name;
        public float nextAttackTime = 0f;
        public int damage;
        //public int damagePaper;
        //public int damageGlass;
        //public int damageMetal;
        public float attackRange;
        public abstract void weaponAttack(Transform ap, LayerMask wie);
    }
    private class hand : weapon
    {
        public override void weaponAttack(Transform ap, LayerMask wie)
        {
            
        }
    }
    private class cardboardCutter : weapon
    {
        public cardboardCutter()
        {
            name = "Cardboard Cutter";
            damage = 1;
            attackRange = 1f;
        }
        public override void weaponAttack(Transform ap, LayerMask wie)
        {
            if(nextAttackTime <= Time.time)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wie);
                foreach (Collider2D enemy in enemiesToDamage)
                {
;                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
                /*Collider2D[] paperEnemiesToDamage = Physics2D.OverlapCircleAll(ap.position, attackRange, wipe);
                foreach (Collider2D enemy in paperEnemiesToDamage)
                {
                    ; enemy.GetComponent<Enemy>().TakeDamage(damagePaper);
                }*/
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
        if(Input.GetKey(KeyCode.Alpha1))// For animations: Conditions -> mainweapon.is (empty, cardboardCutter) ?
        {
            currentWeapon = cardboardcutter;
        }
        if (Input.GetKey(KeyCode.X))
        {
            currentWeapon.weaponAttack(attackPos, whatIsEnemies);
        }
        
    }
}
