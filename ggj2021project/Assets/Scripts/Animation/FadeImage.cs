using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public float Speed = 0.5f;

    private Image image;
    private Color startColor;

    void Start()
    {
        image = GetComponent<Image>();
        startColor = image.material.color;
    }

    public void Show()
    {
        image.material.color = startColor;
    }

    public void StartFadeOut()
    {
        StartCoroutine(DoFadeOut());
    }

    IEnumerator DoFadeOut()
    {
        while(image.material.color.a > 0)
        {
            Color newColor = image.material.color;
            newColor.a -= Time.deltaTime * Speed;
            image.material.color = newColor;
            yield return null;
        }

        yield return null;
    }
}