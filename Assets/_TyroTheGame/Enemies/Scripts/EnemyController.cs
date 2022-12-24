using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Clase que representa las coordenadas
    public List<Vector3> checkpoints = new List<Vector3>();        // Un array de puntos a los que el enemigo tendrá que ir
    int checkpoint;               // para saber a que checkpoint se dirige
    float wanderingSpeed = 5f;

    Quaternion initialRotation;
    NavMeshAgent agent;

    public float speed = 3.5f;
    public float rotationSpeed = 2.0f;

    public bool canPatrol;

    float errorEnPosicionY = 1000f;
    float errorEnPosicionX = 0.5f;

    bool canMove = false;
    Vector3 whereToGo;

   

    void Start()
    {
        canPatrol = true;
        agent = GetComponent<NavMeshAgent>();
        initialRotation = agent.transform.rotation;
        initialForward = transform.forward;

    }

    private void Update()
    {
        if (canPatrol)
        {
            //Debug.Log("Destination:"+ whereToGo);
            whereToGo = GetCheckpoint();
            canMove = Rotation();
            if (canMove)
            {
                agent.speed = agent.speed * TimeManager.instance.globalSpeed;
                Move();
            }
        }
    }



    void Move()
    {
        Vector3 tempVect = whereToGo - this.transform.position;

        tempVect = tempVect.normalized * wanderingSpeed * Time.deltaTime;
        //Debug.Log("CanReach" + agent.hasPath);
        agent.SetDestination(whereToGo);
    }

    private Vector3 initialForward;
    bool Rotation()
    {
        // actually rotates the Gameobjects
        Vector3 usedForRotation = whereToGo - this.transform.position;
        usedForRotation.y = 0;
        usedForRotation = usedForRotation.normalized;
        this.transform.forward = Vector3.Slerp(transform.forward, usedForRotation, rotationSpeed*TimeManager.instance.globalSpeed* Time.deltaTime);
        //

        //used to know if the Gameobject is facing his checkpoint
        Vector3 target = transform.InverseTransformPoint(whereToGo);
        float angle = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;
        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        //

        if (Quaternion.Angle(deltaRotation, initialRotation) < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    int currentIndex = 0;
    Vector3 GetCheckpoint()
    {
        if (ComparePosition(this.transform.position, checkpoints[currentIndex]))
        {
            NextIndexOfArray();
        }
        return checkpoints[currentIndex];
    }
    bool ComparePosition(Vector3 a, Vector3 b)
    {
        if ((Mathf.Abs(a.x - b.x) < errorEnPosicionX) && (Mathf.Abs(a.z - b.z) < errorEnPosicionX) && (Mathf.Abs(a.y - b.y) < errorEnPosicionY))
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    void NextIndexOfArray()
    {
        if (currentIndex + 1 == checkpoints.Count)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
    }
}
