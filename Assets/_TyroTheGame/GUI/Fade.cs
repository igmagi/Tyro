using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    private void Start()
    {
        Debug.Log(GetComponent<CanvasGroup>().alpha);
    }
    public void FadeMe()
    {
        StartCoroutine(DoFade());
    }

    public void UnfadeMe()
    {
        StartCoroutine(Unfade());
    }

    IEnumerator Unfade()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();

        Debug.Log(cg.alpha);
        while (cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        cg.interactable = false;
        yield return null;
    }

    IEnumerator DoFade()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();

        Debug.Log(cg.alpha);
        while (cg.alpha < 1)
        {
            cg.alpha += Time.deltaTime / 1.5f;
            yield return null;
        }
        cg.interactable = false;
        yield return null;

        player.GetComponent<Collider>().enabled = false;

    }
}
