using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _messageUI;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void EnableMessageUI()
    {
        _messageUI.SetActive(true);
    }

    public void UpdateMessageUI(string message)
    {
        Text _messageText = _messageUI.GetComponent<Text>();
        _messageText.text = message;
        StartCoroutine(HideCouroutine());
    }


    IEnumerator HideCouroutine()
    {
        yield return new WaitForSeconds(8f);
        _messageUI.SetActive(false);
    }
}
