using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColliders : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entra: "+ other.gameObject.name);
        other.transform.parent = this.transform;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movement>().isOnMovingPlatform = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Sale: " + other.gameObject.name);

        other.transform.parent = null;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movement>().isOnMovingPlatform = false;
        }

    }
}
