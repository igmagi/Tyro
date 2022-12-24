using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float health;
    // Start is called before the first frame update

    public GameObject tyroCanvas;
    public Slider healthbar;
    CharacterController cc;
    void Start()
    {
        this.checkpoint = this.transform.position;
        health = maxHealth;
        healthbar.value = CalculateHealth();
        cc = this.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = CalculateHealth();

        //if (health < maxHealth) { tyroCanvas.SetActive(true); } //If we only want to show health if it gets hurt
        if (health <= 0) { this.GetComponent<Death>().Die(); this.enabled = false; }
        if (health > maxHealth) { health = maxHealth; }


        // Para probar funcionamiento daño
        /* if (Input.GetKeyDown(KeyCode.T))
         {
             TakeDamage(10);
         }*/
        //Debug.Log(health);
    }
    public void TakeDamage(float dmg)
    {
        health -= 34;
        //TODO: Animations -> (Small jump backwards ATM & Color change(?))
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint") health = 100;

        if (other.tag == "RadioactiveWater")
        {
            health -= 100;
            this.GetComponent<Movement>().setDead(true);
        }

        if (other.tag == "Checkpoint")
        {
            setCheckpoint();
            other.GetComponent<ToggleFX>().Toggle();
            other.enabled = false;
            
        }
    }
    Vector3 checkpoint;

    public Vector3 getCheckpoint()
    {
        return checkpoint;
    }
    public void setCheckpoint()
    {
        checkpoint = this.transform.position;
    }
    private float CalculateHealth()
    {
        return health / maxHealth;
    }

}
