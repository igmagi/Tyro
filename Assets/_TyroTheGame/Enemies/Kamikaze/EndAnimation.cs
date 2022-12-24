using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAnimationFinished()
    {
        this.transform.parent.gameObject.GetComponent<EnemyChase>().setAnimationFinished();
    }
}
