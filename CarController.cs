using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarController : MonoBehaviour
{
   public float forceStrenght = 20000f;
   public float maxTurn = 20f;
    float throttle, steer;
    [Header("WheelColliders")]

    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;

    [Header("WheelTransform")]

    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform backLeftTransform;
    [SerializeField] Transform backRightTransform;

    void Start()
    {

    }
    private void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        Acceleration();
        Steer();

        UpdateWheels(frontLeft, frontLeftTransform);
        UpdateWheels(frontLeft, frontRightTransform);
        UpdateWheels(backLeft, backLeftTransform);
        UpdateWheels(backRight, backRightTransform);
    }
    void Acceleration()
    {
        frontLeft.motorTorque = forceStrenght * Time.deltaTime * throttle;
        frontRight.motorTorque = forceStrenght * Time.deltaTime * throttle;
        backLeft.motorTorque = forceStrenght * Time.deltaTime * throttle;
        backRight.motorTorque = forceStrenght * Time.deltaTime * throttle;
    }
    void Steer()
    {
        frontLeft.steerAngle = maxTurn * steer;
        frontRight.steerAngle = maxTurn * steer;
    }
    void UpdateWheels(WheelCollider wheelCollider,Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position,out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
