using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boss pega a :" + other.gameObject.name);

        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<HealthSystem>().TakeDamage(35);
            other.gameObject.GetComponent<ImpactReceiver>().AddImpact(this.transform.position, 20f);
        }
    }
}
