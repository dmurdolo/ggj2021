using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, 0);
    [SerializeField]
    private float _speed = 5f;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (!_player)
        {
            Debug.LogError("Player object is not present, please be sure it exists and is tagged as Player.");
        }
    }
    private void LateUpdate()
    {       
        // Look
        Quaternion newRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _speed * Time.deltaTime);


        // Move
        Vector3 newPosition = _player.transform.position - _player.transform.forward * _offset.z - _player.transform.up * _offset.y;
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * _speed);
    }
}
