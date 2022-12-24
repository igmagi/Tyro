using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    CharacterController controller;
    public GameObject playerModel;

    public Vector3 groundPosition;
    Vector3 lastGroundPosition;

    public float currentHitDistance;
    public float maxCurrentHitDistance;

    public int groundId;
    public int lastGroundId;

    Quaternion actualRotation;
    Quaternion lastRotation;

    public string mobilePlatformTag = "MobilePlatform";

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        //playerModel = ...
        maxCurrentHitDistance = controller.height;
    }

    void Update()
    {
    
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + new Vector3(0, controller.height, 0), controller.height / 4.2f, -transform.up, out hit))
        {
            GameObject groundedIn = hit.collider.gameObject;
            //Debug.Log(groundedIn.name);
            currentHitDistance = hit.distance;

            if (groundedIn.CompareTag(mobilePlatformTag) && currentHitDistance < controller.height + 0.4)
            {
                //Debug.Log("On Top");
                groundId = groundedIn.GetInstanceID();
                groundPosition = groundedIn.transform.position;

                if (groundId == lastGroundId)
                {
                    if (groundPosition != lastGroundPosition)
                    {
                        //Debug.Log("Position has changed");
                        //Debug.Log("Player: " + controller.transform.position);
                        //Debug.Log(groundPosition);
                        //Debug.Log(lastGroundPosition);
                        //Debug.Log("To be added: " + (groundPosition - lastGroundPosition));

                        Vector3 aux = groundPosition - lastGroundPosition;
                        controller.Move(aux);
                        /*this.transform.position += groundPosition - lastGroundPosition;

                        controller.enabled = false;
                        controller.transform.position = this.transform.position;
                        controller.enabled = true;*/
                        //Debug.Log("Player after adding: " + controller.transform.position);

                    }
                    //ROTATION = PROBLEM
                    /*if (actualRotation != lastRotation)
                    {
                        var newRot = this.transform.rotation * (actualRotation.eulerAngles - lastRotation.eulerAngles);
                        this.transform.RotateAround(groundedIn.transform.position, Vector3.up, newRot.y);
                    }*/
                }

                lastGroundPosition = groundPosition;
                lastGroundId = groundId;
                //lastRotation = actualRotation;
            }
            else
            {
                //Debug.Log("not grounded");
                lastGroundId = -1;
                lastGroundPosition = Vector3.zero;
                lastRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    
    /*
        else if()
        {
            Debug.Log("not grounded");
            lastGroundId = -1;
            lastGroundPosition = Vector3.zero;
            lastRotation = Quaternion.Euler(0, 0, 0);

        }
        */
    }
    
    private void OnDrawGizmos()
    {
        //controller = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position, 1.87f / 4.2f);
    }
    
}
