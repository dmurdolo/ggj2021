using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public GameObject centreOfMass;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _turnSpeed;

    private Rigidbody _rb;


    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centreOfMass.transform.position; 
    }

    // Update is called once per frame
    void Update() {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * vInput * _speed * Time.deltaTime);
        if (vInput != 0) { transform.Rotate(Vector3.up * hInput * _turnSpeed * Time.deltaTime); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            while (_speed > 0)
            {
                _speed -= Time.deltaTime;
            }
        }
    }
}
