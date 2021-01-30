using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCar : MonoBehaviour
{
    public Transform visualCar;
    public Transform centreOfMass;

    public float maxAngle = 30;
    public float maxTorque = 300;

    public float topSpeed = 50; // km per hour
    public float currentSpeed = 0;

    private WheelCollider[] wheels;
    private float pitch = 4f;

    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (WheelCollider wheel in wheels)
        {
            // a simple car where front wheels steer while rear ones drive
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = 0;

            wheel.motorTorque = maxTorque;

            // update visual wheels if any
            {
                Quaternion q;
                Vector3 p;
                wheel.GetWorldPose(out p, out q);

                // assume that the only child of the wheelcollider is the wheel shape
                Transform shapeTransform = visualCar.Find(wheel.name);
                shapeTransform.position = p;
                shapeTransform.rotation = q;
            }
        }
    }

    void FixedUpdate()
    {
        if (currentSpeed > topSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * topSpeed;
        }
    }
}
