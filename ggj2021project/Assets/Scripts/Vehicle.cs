using UnityEngine;

public class Vehicle : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
