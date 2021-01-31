using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject RoadPrefab;

    private GameObject _currentPiece;
    private GameObject _endGameObject;

    // Start is called before the first frame update
    void Start()
    {
        _currentPiece = transform.parent.gameObject;
        _endGameObject = GameObject.Find("EndGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject road = Instantiate(RoadPrefab, transform.position + new Vector3(10f, 0f, 0f), Quaternion.identity);
            road.transform.parent = _endGameObject.transform;

            Destroy(_currentPiece, 10f);
        }
    }
}
