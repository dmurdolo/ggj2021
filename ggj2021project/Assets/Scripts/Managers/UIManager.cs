using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _messageUI;

    public void EnableMessageUI()
    {
        _messageUI.SetActive(true);
        _messageUI.GetComponent<FadeCanvasGroup>().Show();
    }

    public void UpdateMessageUI(string message)
    {
        Text _messageText = _messageUI.GetComponent<Text>();
        _messageText.text = message;
        StartCoroutine(HideCoroutine());
    }

    IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(8f);
        _messageUI.GetComponent<FadeCanvasGroup>().StartFadeOut();
    }
}
