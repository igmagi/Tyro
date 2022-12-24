using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Credits_Button : MonoBehaviour
{
    AudioSource audioSource;
    public void Credits()
    {
        audioSource = GameObject.Find("EventSystem").GetComponent<AudioSource>();
        StartCoroutine(ExecuteSoundAndScene());
    }

    public IEnumerator ExecuteSoundAndScene()
    {
        audioSource.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Creditos");
    }
}
