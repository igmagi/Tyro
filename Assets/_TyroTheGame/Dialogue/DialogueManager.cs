using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    #region singleton
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //Here all references to the dialogue UI elements:
    public GameObject dialogue;
    public Canvas canvas;
    public TextMeshProUGUI text;
    public Image img;
    public bool isThereDialogue = false;
    private bool isThereDialogueWithImageInstruction = true;

    [SerializeField]
    public List<Dialogue> sentences;
    public Sprite[] spriteSentences;
    private List<int> sentencesNotToShowTwice;
    private Animator anim;
    private void Start()
    {
        anim = dialogue.GetComponent<Animator>();
        sentences = new List<Dialogue>();
        sentencesNotToShowTwice = new List<int>();

        sentences.Add(new Dialogue("¡Rápido Tyro, ven <b>aquí!</b>", null, false)); // 0
        sentences.Add(new Dialogue("Consigue la primera <b>gema</b> en la cima del castillo.", null, false));
        sentences.Add(new Dialogue("Observa tus alrededores, mantente siempre atento.", null, false));// 2
        sentences.Add(new Dialogue("<b>Salta</b> para evitar obstáculos y seguir avanzando.", null, false));
        sentences.Add(new Dialogue("¡Cuanto más te concentres, más saltarás!", null, false));
        sentences.Add(new Dialogue("Usa tu <b>doble salto</b> para obstáculos más altos.", null, false));
        sentences.Add(new Dialogue("Usa tu <b>dash</b> para llegar más lejos.", null, false));
        sentences.Add(new Dialogue("Usa tu <b>ataque</b> para reventar.", null, false));
        sentences.Add(new Dialogue("Usa tu <b>magia</b> para ralentizar el tiempo", null, false));

        for (int i = 0; i < sentences.Count(); i++)
        {
            sentences.ElementAt(i).sprite = spriteSentences[i];
        }
    }
    public void ShowDialogue(int id)
    {
        if (!sentencesNotToShowTwice.Contains(id))
        {
            try
            {
                //Debug.Log(sentences.ElementAt(id).msg);
                text.text = sentences.ElementAt(id).msg;
                if (sentences.ElementAt(id).sprite != null)
                {
                    img.sprite = sentences.ElementAt(id).sprite;
                    isThereDialogueWithImageInstruction = true;
                }
                else
                {
                    img.enabled = false;
                    text.rectTransform.localPosition = new Vector3(41, 9, 0);
                    isThereDialogueWithImageInstruction = false;
                }
                if(!isThereDialogue) anim.SetTrigger("PopUp");

                isThereDialogue = true;
                if (sentences.ElementAt(id).displayOnlyOnce)
                {
                    sentencesNotToShowTwice.Add(id);
            }
            } catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        
    }

    public void HideDialogue()
    {
        if (isThereDialogue)
        {
            anim.SetTrigger("Hide");
            if(isThereDialogueWithImageInstruction) img.GetComponent<Animator>().SetTrigger("hide");
            isThereDialogue = false;

        }

    }

    public void ResetDialogue()
    {
       // Debug.Log("reset");
        img.enabled = true;
        text.rectTransform.localPosition = new Vector3(41, 26, 0);
        
        //Debug.Log("fin reset");
    }
}
