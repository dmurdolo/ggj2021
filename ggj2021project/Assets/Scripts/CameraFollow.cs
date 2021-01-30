using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (!_player)
        {
            Debug.LogError("Player object is not present, please be sure it exists and is tagged as Player.");
        }    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + _offset;        
    }
}
