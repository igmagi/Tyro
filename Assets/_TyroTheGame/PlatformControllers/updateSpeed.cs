using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateSpeed : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim == null) return;
        anim.SetFloat("globalSpeed",TimeManager.instance.globalSpeed);
    }

    void setGlobalSpeed()
    {

    }
}
