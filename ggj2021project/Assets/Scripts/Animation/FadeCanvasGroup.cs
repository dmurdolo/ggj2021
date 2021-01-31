using System.Collections;
using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour
{
    public float Speed = 0.5f;

    private CanvasGroup canvasGroup;
    private GameManager _gameManager;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        _gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
    }

    public void StartFadeOut()
    {
        if (GameObject.Find("Dashboard").activeInHierarchy)
        {
            StartCoroutine(DoFadeOut());
        }
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
