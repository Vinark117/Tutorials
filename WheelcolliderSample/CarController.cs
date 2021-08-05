using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [HideInInspector] public float throttleInput; 
    [HideInInspector] public float steerInput;
    [HideInInspector] public float tiltInput; //the Y axis of the joystick, used for airborne controls
    [HideInInspector] public float reverseInput;
    [HideInInspector] public float brakeInput;
    [HideInInspector] public bool airBorne;
    [HideInInspector] public bool onRoad = true;
    [HideInInspector] public bool leftTrack;    //Used for AI training

    public float maxTorque;
    public float maxSteerAngle;
    public float maxBrakeForce;
    public AnimationCurve steeringCurve;
    public float maxSpeed;
    public float antiRoll;
    [Space]
    [SerializeField] private bool playerDriven = false;
    public GameObject sensors; //AI proximity sensor
    
    private Transform centerOfMass;
    public Rigidbody rigidBody { get; private set; }
    private Wheel[] wheels;

    private void Awake()
    {
        //get rigidbody
        rigidBody = GetComponent<Rigidbody>();
        //get all wheels in the children, for top to bottom in the hirearchy
        wheels = gameObject.GetComponentsInChildren<Wheel>();

        //set center of mass
        centerOfMass = transform.Find("CenterOfMass");
        rigidBody.centerOfMass -= (rigidBody.worldCenterOfMass - centerOfMass.position);

        //warning message to avoid off centered gravity
        if (transform.rotation.eulerAngles.x != 0 || transform.rotation.eulerAngles.z != 0) { Debug.LogWarning("The cars center of gravity is off center!"); }
    }

    private void Update()
    {
        //get input
        GetInput();
        //apply input to wheels
        ApplyWheels();

        //set the AI sensor's rotation to horizontal
        if (sensors != null)
        {
            sensors.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    void ApplyWheels()
    {
        //set values
        float speed = rigidBody.velocity.magnitude; //get the speed of the car
        float steer = maxSteerAngle * steerInput * steeringCurve.Evaluate(speed / maxSpeed); //sets the steering based on the steering curve
        float torque = (maxTorque * throttleInput) - (maxTorque / 2 * reverseInput); //applies torque to the wheels, if both forward and backward is pressed, the car will apply half of the max torque
        float brake = maxBrakeForce * brakeInput;

        //debug info
        /*Debug.Log($"Forward torque: {maxTorque * throttleInput}; Reverse torque: {maxTorque / 2 * reverseInput}; Braketorque: {brake};" +
            $"\nSpeed: {speed :0.0} units, {speed / transform.localScale.x * 3.6f :0.0} km/h, {speed / maxSpeed * 100 :0.0}% of max speed");*/

        //apply controls to wheels
        foreach (var w in wheels)
        {
            w.brakeForce = brake;
            w.steerAngle = steer;
            w.motorTorque = torque;
        }

        //check if airborne & on road
        bool air = true;
        bool road = false;
        foreach (var w in wheels)
        {
            if (air)
            {
                air = !w.wheelCollider.isGrounded;
            }
            if (!road)
            {
                WheelHit hit;
                w.wheelCollider.GetGroundHit(out hit);
                if (hit.collider != null) road = hit.collider.CompareTag("Road");
            }
        }
        airBorne = air;
        onRoad = road;
    }

    private void FixedUpdate()
    {
        //antirollbars
        for (int i = 0; i < wheels.Length; i += 2)
        {
            AntiRollBar(wheels[1].wheelCollider, wheels[i + 1].wheelCollider, antiRoll);
        }

        //if in air and pressing break contol car in air
        if (airBorne && brakeInput > 0.1f)
        {
            AirControls();
        }
    }

    void AntiRollBar(WheelCollider WheelL, WheelCollider WheelR, float AntiRoll)
    {
        if (antiRoll <= 0) return;

        WheelHit hit;

        float travelL = 1.0f;
        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL) travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

        float travelR = 1.0f;
        bool groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR) travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * AntiRoll;
        if (groundedL) rigidBody.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);
        if (groundedR) rigidBody.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position);
    }

    void AirControls()
    {
        //set the force depending on the mass of the car
        float flipForce = 0.5f * rigidBody.mass;

        //apply forces at positions
        rigidBody.AddForceAtPosition(transform.up * flipForce, transform.position - (transform.right * steerInput), ForceMode.Force);
        rigidBody.AddForceAtPosition(transform.up * -flipForce, transform.position + (transform.right * steerInput), ForceMode.Force);
        rigidBody.AddForceAtPosition(transform.up * flipForce, transform.position + (transform.forward * tiltInput), ForceMode.Force);
        rigidBody.AddForceAtPosition(transform.up * -flipForce, transform.position - (transform.forward * tiltInput), ForceMode.Force);
    }

    void GetInput()
    {
        if (playerDriven)
        {
            steerInput = InputManager.instance.steerInput;
            tiltInput = InputManager.instance.tiltInput;
            throttleInput = InputManager.instance.throttleInput;
            reverseInput = InputManager.instance.reverseInput;
            brakeInput = InputManager.instance.brakeInput;
        }
        else
        {
            //TODO: AI driven input here
            //its not here lol
            //its in an another script
        }
    }
}
