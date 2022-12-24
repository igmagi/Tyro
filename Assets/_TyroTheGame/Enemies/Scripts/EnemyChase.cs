using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyChase : MonoBehaviour
{

    Ray thugRay;
    RaycastHit rayHit;
    bool follow;
    NavMeshAgent agent;

    public GameObject player;
    public LayerMask layerMask = ~0;

    float chaseSpeed = 10f;

    Animator anim;

    bool isKami = false;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        this.agent = this.GetComponent<NavMeshAgent>();
        follow = false;

        anim = this.GetComponentInChildren<Animator>();
        if (this.GetComponent<ExplosionKamikaze>() != null)
        {
            isKami = true;
        }
        /*
        if (this.GetComponent<Duelista>() != null)
        {
            chaseSpeed = 20;
        }
        if (this.GetComponent<Tanque>() != null)
        {
            chaseSpeed = 15;
        }*/
        /*if (this.GetComponent<Tirador>() != null)
          {
              chaseSpeed = 10;
          }
          */
    }

    Collider[] collidersHit;

    bool hasTransformed = false;
    public bool animationHasFinished = false;
    private void Update()
    {
        thugRay = new Ray(transform.position, transform.forward * 20);

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.tag == "Player") // el objeto a golpear tiene que tener ese tag
            {
                Debug.Log("Le he dado al player!!!!");
                follow = true;
                this.GetComponent<EnemyController>().canPatrol = false;
                if (isKami)
                {
                    agent.speed = 200;
                }
            }
        }

        collidersHit = Physics.OverlapSphere(agent.transform.position, 10);
        for (int i = 0; i < collidersHit.Length; i++)
        {
            if (collidersHit[i].gameObject.tag == "Player")
            {
                follow = true;
                this.GetComponent<EnemyController>().canPatrol = false;
                if (isKami)
                {
                    agent.speed = 200;
                }
            }
        }

        if (!hasTransformed && follow)
        {
            anim.SetTrigger("ModoDiablo");
            hasTransformed = true;
        }

        if (animationHasFinished)
        {
            ChasePlayer();
        }
    }

    public void setAnimationFinished()
    {
        animationHasFinished = true;
    }

    void ChasePlayer()
    {
        this.GetComponent<EnemyController>().canPatrol = false;

        moveToPlayer();
        facePlayer();

        if (Vector3.Distance(this.transform.position, player.transform.position) > 15)
        {
            this.GetComponent<EnemyController>().canPatrol = true;
            follow = false;
        }
    }



    void moveToPlayer()
    {
        Vector3 tempVect = player.transform.position - this.transform.position;

        tempVect = tempVect.normalized * chaseSpeed * Time.deltaTime;

        agent.SetDestination(player.transform.position);
    }

    void facePlayer()
    {
        Vector3 usedForRotation = player.transform.position - this.transform.position;
        usedForRotation.y = 0;
        usedForRotation = usedForRotation.normalized;
        transform.forward = Vector3.Slerp(transform.forward, usedForRotation, 2.0f * Time.deltaTime);
    }
}