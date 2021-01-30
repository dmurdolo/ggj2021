using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
    public TextAsset[] narrativeText;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (!_uiManager)
        {
            Debug.LogError("Could not load UI Manager.");
        }
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void DisplayCheckpointNarrative(int currentCheckpoint)
    {
        _uiManager.EnableMessageUI();
        _uiManager.UpdateMessageUI(narrativeText[currentCheckpoint].text);
    }
}
