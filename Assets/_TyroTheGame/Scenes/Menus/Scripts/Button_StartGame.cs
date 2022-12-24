using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Button_StartGame : MonoBehaviour {
    AudioSource audioSource;
    public void Play_Game()
    {
        audioSource = GameObject.Find("EventSystem").GetComponent<AudioSource>();
        StartCoroutine(ExecuteSoundAndScene());
    }

    public IEnumerator ExecuteSoundAndScene()
    {
        audioSource.Play();
        yield return new WaitForSeconds(1);
        GameManager.GetInstance().nextScene = "NivelUno";
        SceneManager.LoadScene("Load_Scene");
    }
}
