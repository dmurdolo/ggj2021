using UnityEngine;
using System.Collections;
public class PhysicsCarController : MonoBehaviour
{
    public Transform visualCar;
    public Transform centreOfMass;
    
    public float maxAngle = 30;
    public float maxTorque = 300;

    public float topSpeed = 100; // km per hour
    public float currentSpeed = 0;
    public GameObject CarEngine;

    private float pitch = 4f;
    private WheelCollider[] wheels;

    [SerializeField]
    private Light[] lights;

    [SerializeField]
    private float brakes = 0f;
    [SerializeField]
    private float brakesPower = 500f;

    // here we find all the WheelColliders down in the hierarchy
    public void Start()
    {
        wheels = GetComponentsInChildren<WheelCollider>();
        GetComponent<Rigidbody>().centerOfMass = centreOfMass.localPosition;
    }

    // this is a really simple approach to updating wheels
    // here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
    // this helps us to figure our which wheels are front ones and which are rear
    public void Update()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        float angle = maxAngle * hInput;
        float torque = maxTorque * vInput;

        // Brakes!
        if (Input.GetKey(KeyCode.Space))
        {
            brakes = brakesPower;
            EnableBrakeLights(true);
        }
        else
        {
            brakes = 0f;
            EnableBrakeLights(false);
        }

        if (vInput < 0)
        {
            EnableBrakeLights(true);
        }

        foreach (WheelCollider wheel in wheels)
        {
            // a simple car where front wheels steer while rear ones drive
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = angle;

            wheel.motorTorque = torque;

            // apply the brakes!
            wheel.brakeTorque = brakes;

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
        CarEngine.GetComponent<AudioSource>().pitch = pitch + 0.5f;
    }

    void FixedUpdate()
    {
        if (currentSpeed > topSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * topSpeed;
        }
    }

    void EnableBrakeLights(bool on)
    {
        lights[0].GetComponent<Light>().enabled = on;
        lights[1].GetComponent<Light>().enabled = on;
    }
}