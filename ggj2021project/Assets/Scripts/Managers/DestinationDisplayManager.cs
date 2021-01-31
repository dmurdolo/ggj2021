using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DestinationDisplayManager : MonoBehaviour
{
    public GameObject DestinationDisplay;
    public Material[] DestinationMaterials;

    public void SetCheckpoint(int checkpoint)
    {
        DestinationDisplay.GetComponent<FadeImage>().Show();
        DestinationDisplay.GetComponent<Image>().material = DestinationMaterials[checkpoint];
        StartCoroutine(HideCoroutine());
    }

    IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(8f);
        DestinationDisplay.GetComponent<FadeImage>().StartFadeOut();
    }
}
