using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Break(float fadeTime)
    {
        StartCoroutine(FadeAway(fadeTime));
    }

    private IEnumerator FadeAway(float fadeTime)
    {
        Color fadeColor = spriteRenderer.color;
        float t = 0;
        while (t < fadeTime)
        {
            fadeColor.a = Mathf.Lerp(1f, 0, t / fadeTime);
            spriteRenderer.color = fadeColor;
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
