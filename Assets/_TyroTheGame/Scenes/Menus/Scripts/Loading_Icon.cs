using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_Icon : MonoBehaviour {
    
    public float speed = -200f;

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
    }
}
