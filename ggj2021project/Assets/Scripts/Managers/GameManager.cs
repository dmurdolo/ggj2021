using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CheckpointManager checkpointManager;

    [SerializeField]
    private GameObject[] _hideObjects;
    [SerializeField]
    private GameObject _endGameArea;

    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = GameObject.Find("Managers").GetComponent<CheckpointManager>();

        if (!checkpointManager)
        {
            Debug.LogError("Couldn't load Checkpoint Manager");
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (checkpointManager.GetCurrentCheckpoint() == checkpointManager.Checkpoints.Length-1)
        {
            _endGameArea.SetActive(true);

            foreach (GameObject obj in _hideObjects)
            {
                obj.SetActive(false);
            }
        }
        
    }
}