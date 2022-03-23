using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public Renderer thisRenderer;

    public void Awake()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        Debug.Log("fadeInStart");
        for (float alpha = 0f; alpha < 1; alpha += 0.01f)
        {
            thisRenderer.material.SetFloat("_ALPHA", alpha);
            Debug.Log("fadein" + alpha);
            yield return new WaitForSeconds(.01f);
        }
    }
}
