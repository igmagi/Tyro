using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("TRIGGER");
            DialogueManager.instance.ShowDialogue(index);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogueManager.instance.HideDialogue();
            //Debug.Log("SALIR TRIGGER");
        }
    }
}
