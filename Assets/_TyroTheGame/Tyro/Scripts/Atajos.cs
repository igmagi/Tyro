using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atajos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.GetComponent<CharacterController>().enabled = false;
            this.transform.position = new Vector3(80,40,167);
            this.GetComponent<CharacterController>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.GetComponent<CharacterController>().enabled = false;
            this.transform.position = new Vector3(350, 88, 332);
            this.GetComponent<CharacterController>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.GetComponent<CharacterController>().enabled = false;
            this.transform.position = new Vector3(350, 88, 332);
            this.GetComponent<CharacterController>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.GetComponent<CharacterController>().enabled = false;
            this.transform.position = new Vector3(375, 209, 98);
            this.GetComponent<CharacterController>().enabled = true;
        }
    }
}
