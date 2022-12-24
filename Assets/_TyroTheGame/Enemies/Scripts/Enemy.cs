using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public GameObject particles;


    private void Start()
    {
        health = maxHealth;
        //healthbar.value = CalculateHealth();
    }

    private void Update()
    {


       // healthbar.value = CalculateHealth();

       // if (health < maxHealth) { enemyCanvas.SetActive(true); } //If we only want to show health if it gets hurt
        if (health <= 0) { Die(); }
        if (health > maxHealth) { health = maxHealth; }

        //Debug.Log(health/maxHealth);

    }

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.SetActive(false);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        //TODO: Animations -> (Small jump backwards ATM & Color change(?))
    }
    private void Die()
    {
        //Debug.Log("ded");
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private float CalculateHealth()
    {
        return health / maxHealth;
    }
}
