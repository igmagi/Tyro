using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosionKamikaze : MonoBehaviour
{

    public float liveTime = 20f; // se debe cambiar a cuando se acerca a Tyro (ahora esta puesto para que explote en 3 seg aprox)
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float force = 20f;

    public GameObject player;

    public bool reset = false;

    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = this.transform.position;
        player = GameObject.FindWithTag("Player");
        // countdown = delay;
    }
    float timeToBeDestroyed;
    bool saved = false;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 2)
        {
            Explode();
        }

        if (this.GetComponent<EnemyChase>().animationHasFinished && !saved)
        {
            timeToBeDestroyed = Time.time + liveTime;
            saved = true;
        }

        if (saved && Time.time > timeToBeDestroyed)
        {
            ExplodeFX();
        }

        if (reset)
        {
            Reset();
        }
    }

    void Explode()
    {
        player.GetComponent<ImpactReceiver>().AddImpact(this.transform.position, force);
        player.GetComponent<HealthSystem>().TakeDamage(40f);

        Instantiate(explosionEffect, transform.position, transform.rotation);
        this.GetComponent<EnemyChase>().enabled = false;
        //this.gameObject.SetActive(false);

        this.GetComponent<EnemyChase>().enabled = false;
        this.GetComponent<EnemyController>().enabled = false;
        this.gameObject.transform.position = Vector3.zero;
        //Destroy(this.gameObject);
    }
    public void ExplodeFX()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        this.GetComponent<EnemyChase>().enabled = false;
        this.GetComponent<EnemyController>().enabled = false;
        this.gameObject.transform.position = Vector3.zero;
        //this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        //this.gameObject.SetActive(true); 
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<EnemyChase>().enabled = true;
        this.transform.position = initialPosition;
        this.GetComponent<EnemyChase>().animationHasFinished = false;
        this.saved = false;
        this.gameObject.transform.position = initialPosition;
    }

}
