using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TiradorProjectile : MonoBehaviour
{
    GameObject target = null;
    public float damage = 20f;

    public float speed = 20f;

    public Vector3 dir;
    public float impactForce = 10f;

    private float timeAlive;
    private float lifeSpan = 8f;

    CharacterController cc;

    private void Start()
    {
        cc = this.GetComponent<CharacterController>();
        timeAlive = Time.time + lifeSpan;
    }
    void Update()
    {
        if (Time.time > timeAlive)
        {
            Destroy(gameObject);
            return;
        }
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        float distanceThisFrame = speed * TimeManager.instance.globalSpeed * Time.deltaTime;
        cc.Move(dir.normalized * distanceThisFrame);

        /*
        if (target != null && Vector3.Distance(this.transform.position, target.transform.position) < 0.2f)
        {
            Destroy(gameObject);
        }
        */
    }

    public void Shoot(GameObject _target)
    {
        target = _target;
        dir = (target.transform.position) - transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Bala colisiona con " + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            Enemy x = other.gameObject.GetComponent<Enemy>();
            if (x == null) return;
            x.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            HealthSystem x = other.gameObject.GetComponent<HealthSystem>();
            if (x == null) return;
            x.TakeDamage(damage);            
            other.gameObject.GetComponent<ImpactReceiver>().AddImpact(this.transform.position, impactForce);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this);
    }

    public void HitTarget()
    {
        Enemy x = target.GetComponent<Enemy>();
        if (x != null)
        {
            x.TakeDamage(damage);
        }
        //Efecto de particulas
        Destroy(gameObject);
    }

}
