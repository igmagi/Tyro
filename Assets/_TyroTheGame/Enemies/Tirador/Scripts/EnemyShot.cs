using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {

    public float timeLeftToShoot;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform jugador;
    
    // Start is called before the first frame update
    void Start() {
        timeLeftToShoot = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update() {
        
        if (timeLeftToShoot <= 0 && (Vector3.Distance(transform.position,jugador.position)<30)) {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeLeftToShoot = startTimeBtwShots;
        } else {
            timeLeftToShoot = timeLeftToShoot - Time.deltaTime * TimeManager.instance.globalSpeed;
        }
    }
}
