using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Exit : MonoBehaviour {

    // Update is called once per frame
    public void Exit_Game() {
        Application.Quit();
        Debug.Log("The game is closed");
    }
}
