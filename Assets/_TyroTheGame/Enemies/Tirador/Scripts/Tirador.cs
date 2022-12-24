using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Tirador : MonoBehaviour
{

    public GameObject target;
    public float range = 10f;
    public float dist;

    public float fireRate = 1f;
    public float fireCountDown = 0;

    public GameObject projectile;
    public Transform firePoint;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //target = GameObject.FindWithTag("Player");
        fireCountDown = 1f / fireRate;
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist < range)
        {
            if (target.GetComponent<HealthSystem>() != null)
            {
                if (target.GetComponent<HealthSystem>().health > 0)
                {
                    GetComponent<NavMeshAgent>().isStopped = true;
                    this.GetComponent<Animator>().enabled = false;
                    Rotate();
                }
                else
                {
                    GetComponent<NavMeshAgent>().isStopped = false;
                    GetComponent<Animator>().enabled = true;
                }
            }
            else
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                this.GetComponent<Animator>().enabled = false;
                Rotate();
            }
           
        }
        else
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<Animator>().enabled = true;
        }
    }

    void Rotate()
    {
        Vector3 dir = target.gameObject.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        lookRotation.x = 0; lookRotation.z = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10 * TimeManager.instance.globalSpeed);

        if (fireCountDown < 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime * TimeManager.instance.globalSpeed;
    }

    void Shoot()
    {
        GameObject x = (GameObject)Instantiate(projectile, firePoint.position, firePoint.rotation);
        TiradorProjectile c = x.GetComponent<TiradorProjectile>();
        if (c != null)
        {
            c.Shoot(target);
            audioSource.Play();
        }
    }
}
