using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loading : MonoBehaviour {

    public float timeMinimum = 2f;
    GameManager gameManager;
    AsyncOperation load;

    // Start is called before the first frame update
    void Start() {
        gameManager = GameManager.GetInstance();
        load = SceneManager.LoadSceneAsync(gameManager.nextScene);
        load.allowSceneActivation = false;
        StartCoroutine(LoadList());
        Time.timeScale = 1;
    }

    IEnumerator LoadList(){
        yield return new WaitForSeconds(timeMinimum);
        load.allowSceneActivation = true;
    }
}
