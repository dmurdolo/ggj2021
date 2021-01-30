using UnityEngine;

public class Rotate : MonoBehaviour {

	public float Speed = 1.0f;

	void Update () {
        transform.Rotate(Vector3.up, Time.deltaTime * this.Speed);
	}
}
