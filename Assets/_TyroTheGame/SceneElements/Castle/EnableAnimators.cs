using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimators : MonoBehaviour
{

    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void enableAllAnimators()
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if(obj.transform.GetChild(i).GetComponent<Animator>() != null)
            {
                obj.transform.GetChild(i).GetComponent<Animator>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enableAllAnimators();
            this.enabled = false;
        }
    }
}
