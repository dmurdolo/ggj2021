using UnityEngine;

public class CarEndGame : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    public void StartEngine(float speed)
    {
        _speed = speed;
    }

}
