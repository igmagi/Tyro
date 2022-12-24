using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController cc;

    Collider[] collidersHit;
    List<Collider> colldiers = new List<Collider>();
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Movement>().isDashing == true)
        {
           
            collidersHit = Physics.OverlapSphere(cc.transform.position, 1);
            for (int i = 0; i < collidersHit.Length; i++)
            {
                if(collidersHit[i].gameObject.tag == "Enemy")
                {
                    colldiers.Add(collidersHit[i]);
                }
            }

            if (colldiers.Count > 0)  
            {
                for (int i = 0; i < colldiers.Count; i++)
                {
                    colldiers[i].gameObject.GetComponent<CharacterController>().enabled = false;
                }
            }

        }

        if (this.GetComponent<Movement>().isDashing == false)
        {
            if (colldiers.Count > 0)
            {
                for (int i = 0; i < colldiers.Count; i++)
                {
                    colldiers[i].gameObject.GetComponent<CharacterController>().enabled = true;
                }
                colldiers.Clear();
            }

        }
    }

    /* private void OnTriggerEnter(Collider other)
     {
         Debug.Log("HE COLISIONAAAAAAAAO");
         if(this.GetComponent<ThirdPersonMovementController>().dashing == true)
         {
             other.GetComponent<CharacterController>().enabled = false;
         }
     }
     private void OnTriggerStay(Collider other)
     {
         if (this.GetComponent<ThirdPersonMovementController>().dashing == true)
         {
             other.GetComponent<CharacterController>().enabled = false;
         }
     }
     private void OnTriggerExit(Collider other)
     {
         Debug.Log("HE COLISIONAAAAAAAAO");
         other.GetComponent<CharacterController>().enabled = true;
     }*/
}
