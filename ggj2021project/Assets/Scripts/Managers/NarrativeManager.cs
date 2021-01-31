using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
    public TextAsset[] narrativeText;

    private UIManager _uiManager;
    
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (!_uiManager)
        {
            Debug.LogError("Could not load UI Manager.");
        }
    }

    public void DisplayCheckpointNarrative(int currentCheckpoint)
    {
        _uiManager.EnableMessageUI();
        _uiManager.UpdateMessageUI(narrativeText[currentCheckpoint].text);
    }
}
