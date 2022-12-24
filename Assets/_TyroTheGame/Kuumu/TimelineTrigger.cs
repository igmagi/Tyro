using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector director;
    Animator animator;
    public Transform mainCamera;
    public Transform virtualCamera;
    int index;
   
 void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("TRIGGER");
            virtualCamera.position = mainCamera.position;
            virtualCamera.rotation = mainCamera.rotation;
            director.Play();
            
           
        }
        
    }

}
