using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio_Buttons : MonoBehaviour
{
    AudioSource source;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("EventSystem").GetComponent<AudioSource>();
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(Prueba);
    }

    private void Prueba()
    {
        source.Play();
    }
}
