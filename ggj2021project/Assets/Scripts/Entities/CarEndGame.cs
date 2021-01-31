using UnityEngine;

public class CarEndGame : MonoBehaviour
{
    public float Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
