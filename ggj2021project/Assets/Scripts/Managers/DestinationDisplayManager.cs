using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DestinationDisplayManager : MonoBehaviour
{
    public GameObject DestinationDisplay;
    public Material[] DestinationMaterials;

    private Color startColor;
    
    void Start()
    {
        startColor = DestinationMaterials[0].color;
    }

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

    private void OnDestroy()
    {
        // Reset the colour & alpha after all the fading
        foreach(Material mat in DestinationMaterials)
        {
            mat.color = startColor;
        }
    }
}
