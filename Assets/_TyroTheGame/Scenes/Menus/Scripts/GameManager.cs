using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public string nextScene;
    private static GameManager instance;

    public static GameManager GetInstance() {
        return instance;
    }

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1;
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
