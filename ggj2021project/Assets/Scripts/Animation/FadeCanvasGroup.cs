using System.Collections;
using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour
{
    public float Speed = 0.5f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
    }

    public void StartFadeOut()
    {
        StartCoroutine(DoFadeOut());
    }

    IEnumerator DoFadeOut()
    {
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * Speed;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;
    }
}
