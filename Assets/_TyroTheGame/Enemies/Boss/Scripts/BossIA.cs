using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BossIA : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anim;
    public Transform player;
    AudioSource audio;

    Enemy ene;
    void Start()
    {
        ene = this.GetComponent<Enemy>();
        anim = this.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position,transform.position)< 5 && player.gameObject.GetComponent<HealthSystem>().health>0)
        {
           
            int number = Random.Range(1, 4);
            Debug.Log(number);
            switch (number)
            {
                case 1:
                    audio.Play();
                    anim.SetTrigger("AtaqueSimple");
                    break;
                case 2:
                    audio.Play();
                    anim.SetTrigger("AtaqueSimple2");
                    break;
                case 3:
                    anim.SetTrigger("AtaqueFuerte");
                    break;
            }
        }
        
    }
    private void OnDestroy()
    {
        player.GetComponent<Win>().win = true;
    }
  


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boss pega a :" + other.gameObject.name);

        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<HealthSystem>().TakeDamage(35);
            other.gameObject.GetComponent<ImpactReceiver>().AddImpact(this.transform.position,20f);
        }
    }
 
}
