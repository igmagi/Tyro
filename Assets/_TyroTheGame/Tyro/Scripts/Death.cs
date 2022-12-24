using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public Canvas canvas;
    public bool fadeToBlackEnded = false;
    public CinemachineBrain cb;
   

    public bool fadeToNormalEnded = false;


    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
      
    }


    public void Die()
    {
        StartCoroutine(animateDeath());
    }
    
    private IEnumerator animateDeath()
    {
        anim.SetTrigger("death");
        TimeManager.instance.DoSlowMo();
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<Movement>().enabled = false;
        this.GetComponent<BasicAttack>().enabled = false;
        cb.enabled = false;
        yield return new WaitForSeconds(2);

        canvas.GetComponent<Fade>().FadeMe();
        TimeManager.instance.RestoreNormalTime();
        yield return new WaitForSeconds(2);

        this.GetComponent<Movement>().enabled = true;
        this.GetComponent<BasicAttack>().enabled = true;
        cb.enabled = true;
        this.GetComponent<HealthSystem>().enabled = true;
        this.GetComponent<HealthSystem>().health = this.GetComponent<HealthSystem>().maxHealth;
        this.transform.position = this.GetComponent<HealthSystem>().getCheckpoint();
        this.GetComponent<CharacterController>().enabled = true;

        canvas.GetComponent<Fade>().UnfadeMe();

        this.GetComponent<Movement>().setDead(false);

    }
   

    // Update is called once per frame
    void Update()
    {

    }
}
