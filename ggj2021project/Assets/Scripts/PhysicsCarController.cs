using UnityEngine;
using System.Collections;
public class PhysicsCarController : MonoBehaviour
{
    private WheelCollider[] wheels;

    public float maxAngle = 30;
    public float maxTorque = 300;

    public float topSpeed = 100; // km per hour
    public float currentSpeed = 0;
    private float pitch = 4f;

    public Transform visualCar;

    //public GameObject CarEngine;

    // here we find all the WheelColliders down in the hierarchy
    public void Start()
    {
        wheels = GetComponentsInChildren<WheelCollider>();
        GetComponent<Rigidbody>().centerOfMass = visualCar.localPosition;
    }

    // this is a really simple approach to updating wheels
    // here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
    // this helps us to figure our which wheels are front ones and which are rear
    public void Update()
    {
        float angle = maxAngle * Input.GetAxis("Horizontal");
        float torque = maxTorque * Input.GetAxis("Vertical");

        foreach (WheelCollider wheel in wheels)
        {
            // a simple car where front wheels steer while rear ones drive
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = angle;

            //if (wheel.transform.localPosition.z < 0)
            wheel.motorTorque = torque;

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

        // CURRENT SPEED
        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        pitch = currentSpeed / topSpeed;
        //CarEngine.GetComponent<AudioSource>().pitch = pitch + 0.5f;
    }

    public void FixedUpdate()
    {
        if (currentSpeed > topSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * topSpeed;
        }
    }
}