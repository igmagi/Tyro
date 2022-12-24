using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFX : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blanco;
    public GameObject morado;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        blanco.SetActive(false);
        morado.SetActive(true);

    }
}
