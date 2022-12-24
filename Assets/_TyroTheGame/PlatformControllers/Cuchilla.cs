using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuchilla : MonoBehaviour
{
    void Start()
    {

    }

    Vector3 pos;
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Animator>().SetFloat("speed", TimeManager.instance.globalSpeed);
    }
}
