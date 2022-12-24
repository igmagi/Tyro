using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksPosition : MonoBehaviour

{
    //Tenemos q tener en cuenta la rotacion del kamikaze y la posicion en el transform, sabemos q la posicion se fastidia cuando la posicion se cambia. Tenemos q haber acabado de hacer todos los cambios y movimientos antes de q el objeto empizde a rotar
    //La idea de marcelo es comparar posiciones mientras la rotacion no cambie
    
    GameObject sparks;
    Vector3 sparksPosition;
    Vector3 direccion;
    Vector3 posicionFional;
    public GameObject kami;

    Quaternion initRotation;
    // Start is called before the first frame update
    void Start()
    {
        sparks = this.gameObject;
        sparksPosition = this.transform.position;

        posicionFional = new Vector3(sparksPosition.x, sparksPosition.y, sparksPosition.z - 0.5f);

        direccion = Vector3.Normalize(posicionFional - sparksPosition);

        initRotation = kami.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // restar 1 en z


        //Debug.Log("Direccion:" + direccion);

        if (initRotation == kami.transform.rotation)
        {
            this.transform.Translate(direccion * Time.deltaTime/2);
        }



    }


}
