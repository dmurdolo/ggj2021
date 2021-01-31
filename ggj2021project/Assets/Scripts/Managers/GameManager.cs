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
    [SerializeField]
    private GameObject _fatherVehicle;
    [SerializeField]
    private GameObject _endFatherVehicle;
    [SerializeField]
    private GameObject _playerVehicle;

    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = GetComponent<CheckpointManager>();

        if (!checkpointManager)
        {
            Debug.LogError("Couldn't load Checkpoint Manager");
        }

        // Be sure these objects are displaying while regular game mode
        foreach (GameObject obj in _hideObjects)
        {
            obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpointManager.IsCheckpointComplete(5))
        {
            StartCoroutine(StartEndGame());
        }        
    }
    
    public void EnableAutoDrive()
    {
        _playerVehicle.GetComponent<PhysicsCarController>().enabled = false;
        _playerVehicle.GetComponent<PhysicsCarControllerEndGame>().enabled = true;
        _playerVehicle.transform.Find("Trail_Left").gameObject.SetActive(false);
        _playerVehicle.transform.Find("Trail_Right").gameObject.SetActive(false);
        _endFatherVehicle.GetComponent<CarEndGame>().enabled = true;
    }

    IEnumerator StartEndGame()
    {
        yield return new WaitForSeconds(2f);

        _endGameArea.SetActive(true);
        _endFatherVehicle.GetComponent<CarEndGame>().enabled = false;
        _fatherVehicle.SetActive(false);
        _endFatherVehicle.SetActive(true);

        foreach (GameObject obj in _hideObjects)
        {
            obj.SetActive(false);
        }
    }
}
