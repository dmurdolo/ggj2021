using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private GameObject _dashboard;
    [SerializeField]
    private GameObject _credits;
    [SerializeField]
    private GameObject _music;
    [SerializeField]
    private AudioClip outro;

    public bool _gameComplete = false;

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
        
        // Go back to start menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    public void EnableAutoDrive()
    {
        _playerVehicle.GetComponent<PhysicsCarController>().enabled = false;
        _playerVehicle.GetComponent<PhysicsCarControllerEndGame>().enabled = true;
        _playerVehicle.transform.Find("Trail_Left").gameObject.SetActive(false);
        _playerVehicle.transform.Find("Trail_Right").gameObject.SetActive(false);

        _music.GetComponent<AudioSource>().clip = outro;
        _music.GetComponent<AudioSource>().volume = 1.0f;
        _music.GetComponent<AudioSource>().Play();
    }

    IEnumerator StartEndGame()
    {
        yield return new WaitForSeconds(4f);

        _endGameArea.SetActive(true);
        _fatherVehicle.SetActive(false);
        _endFatherVehicle.SetActive(true);
        _dashboard.SetActive(false);

        _gameComplete = true;

        foreach (GameObject obj in _hideObjects)
        {
            obj.SetActive(false);
        }

        yield return new WaitForSeconds(8f);

        _credits.SetActive(true);
    }
}
