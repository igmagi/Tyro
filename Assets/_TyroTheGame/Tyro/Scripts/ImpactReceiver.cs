using UnityEngine;

using System.Collections;

public class ImpactReceiver : MonoBehaviour
{
    public float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    private CharacterController character;
    private Animator anim;
    // Use this for initialization
    void Start()
    {
        character = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2F)
        {
            character.Move(impact * Time.deltaTime);
            
        }
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force)
    {
        Debug.Log("Recibien impacto");
        Vector3 aux = this.transform.position-dir;
        aux.Normalize();
        if (aux.y < 0) aux.y = -aux.y; // reflect down force on the ground
        impact += aux.normalized * force / mass;
        anim.SetTrigger("knockback");
        this.GetComponent<Movement>().Rotate(-aux);
    }
}