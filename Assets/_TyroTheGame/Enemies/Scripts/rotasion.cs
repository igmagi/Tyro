using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotasion : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        facePlayer();
    }
    void facePlayer()
    {

        Vector3 dir = player.gameObject.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        lookRotation.x = 0; lookRotation.z = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10 * TimeManager.instance.globalSpeed);

    }
}
