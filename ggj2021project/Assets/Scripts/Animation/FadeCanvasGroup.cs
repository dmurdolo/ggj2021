using System.Collections;
using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour
{
    public float Speed = 0.5f;

    public void StartFade()
    {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * Speed;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;
    }
}
