using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    [SerializeField] bool steering = false;
    [SerializeField] bool inverseSteering = false;
    [SerializeField] bool isMotor = false;
    [SerializeField] bool isBrake = true;

    [HideInInspector] public float steerAngle;
    [HideInInspector] public float motorTorque;
    [HideInInspector] public float brakeForce;

    public Transform wheelTransform { get; private set; }
    public WheelCollider wheelCollider { get; private set; }

    float wheelOffset;

    private void Start()
    {
		//get the collider
        wheelCollider = GetComponent<WheelCollider>();
		//get the wheel model
        wheelTransform = GetComponentInChildren<MeshRenderer>().transform; //the transform needs to be a child of the wheel
        //get the wheel position
		wheelOffset = wheelTransform.localPosition.x * transform.lossyScale.x;
    }

    private void Update()
    {
        //wheel pos and rotation
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos + transform.right * wheelOffset;
        wheelTransform.rotation = rot;

        //steering
        if (steering) wheelCollider.steerAngle = steerAngle * (inverseSteering ? -1 : 1);

        //debug
        //Debug.Log($"Spin on {transform.name}: {wheelCollider.rpm :0.00}rpm; supported mass: {wheelCollider.sprungMass :0.00}");
    }

    private void FixedUpdate()
    {
        //motor
        if (isMotor) wheelCollider.motorTorque = motorTorque;

        //brakes
        if (isBrake) wheelCollider.brakeTorque = brakeForce;
    }
}
