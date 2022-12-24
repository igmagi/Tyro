using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 1.2f;
    public int attackDamage = 55;

    public float attackRate = 2.3f;
    float nextAttackTime = 0f;

    
    private Animator anim;

    public ParticleSystem ps;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        // When the time of attack is finish, Tyro can attack
        if (Time.time >= nextAttackTime)
        {
            // When you click on left click of mouse
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Tyro attack to enemies
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("attack");
        ps.Play();
        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        //DrawRange();
        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);

            /*if (aux)
            {
                enemy.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                aux = false;
            }
            else
            {
                enemy.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                aux = true;
            }
            */


            /// TODO: Add this line of code when we implement the script of enemy
            if (enemy.GetComponent<Enemy>() != null)
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void DrawRange()
    {
        //anim.Play("mele_swing");
    }
}
