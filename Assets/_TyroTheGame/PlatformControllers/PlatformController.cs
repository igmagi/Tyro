using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Rigidbody platformRB;
    
    public Transform[] positions;
    public float speed;
    public int actualPosition = 0;
    public int nextPosition = 1;

    void FixedUpdate()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, positions[nextPosition].position, speed * Time.deltaTime));
        if(Vector3.Distance(platformRB.position, positions[nextPosition].position) <= 0)
        {
            actualPosition = nextPosition;
            nextPosition++;
            if(nextPosition >= positions.Length)
            {
                nextPosition = 0;
            }
        }
    }
}
