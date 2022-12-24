using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Button_SelectLevel : MonoBehaviour
{
    AudioSource audioSource;
    public void LevelSelector()
    {
        audioSource = GameObject.Find("EventSystem").GetComponent<AudioSource>();
        StartCoroutine(ExecuteSoundAndScene());
    }

    public IEnumerator ExecuteSoundAndScene()
    {
        audioSource.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SelectorNivel");
    }
}
