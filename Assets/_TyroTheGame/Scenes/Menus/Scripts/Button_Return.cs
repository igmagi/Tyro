using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Button_Return : MonoBehaviour
{
    AudioSource audioSource;
    public void Return()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        StartCoroutine(ExecuteSoundAndScene());
    }

    public IEnumerator ExecuteSoundAndScene()
    {
        audioSource.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Menu");
    }
}
