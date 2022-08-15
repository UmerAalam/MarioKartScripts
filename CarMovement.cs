using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Variables")]
    private float currentSpeed = 0f;
    [SerializeField] float realSpeed = 1f;
    [SerializeField] float rotationTime = 1f;
    private float rotationSpeed = 0f;
    [SerializeField] float maxTurn = 20f;
    [SerializeField] float carRotationSpeed = 20f;
    private float horizontal,vertical;
    [SerializeField] Rigidbody carRb;
    [Header("Tires")]
    [Header("All Tires")]
    [SerializeField] Transform[] allTires;
    [Header("Front Tires")]
    [SerializeField] Transform[] frontTires;
    private Vector3 moveForward;
    private Vector3 leftRightTurn;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //Stop and Run Car Rotation
        if (vertical > 0)
            currentSpeed = 1f;
        else if (vertical < 0)
            currentSpeed = 1f;
        else if (vertical == 0)
            currentSpeed = 0f;
        // Wheel Rotation
        rotationSpeed = Mathf.Clamp(rotationSpeed,-maxTurn,maxTurn);
        if (horizontal > 0)
            rotationSpeed += rotationTime * Time.deltaTime;
        if (horizontal < 0)
            rotationSpeed -= rotationTime * Time.deltaTime;
        if(horizontal == 0)
        {
            if(rotationSpeed > 0)
            {
                while(rotationSpeed <= 0)
                {
                    rotationSpeed = 0f;
                    rotationSpeed -= rotationTime * Time.deltaTime;
                }
            }
            if (rotationSpeed < 0)
            {
                while(rotationSpeed >= 0)
                {
                    rotationSpeed = 0f;
                    rotationSpeed += rotationTime * Time.deltaTime;
                }
            }

        }
    }
    void FixedUpdate()
    {
        ForwardBackward();
        LeftRightMovement();
        RotateTires();
        FrontWheelRotation();
    }
    void ForwardBackward()
    {
       // moveForward = Vector3.forward * currentSpeed * realSpeed * vertical * Time.deltaTime;
        //carRb.AddRelativeForce(moveForward);
    }
    void LeftRightMovement()
    {
        leftRightTurn = Vector3.up * carRotationSpeed * horizontal * vertical * Time.deltaTime;
        transform.Rotate(leftRightTurn);
    }
    void RotateTires()
    {
        foreach(Transform wheels in allTires)
        {
            wheels.transform.Rotate(Vector3.right * realSpeed * currentSpeed * vertical * Time.deltaTime);
        }
    }
    void FrontWheelRotation()
    {
        foreach(Transform rotationWheels in frontTires)
        {
            if (rotationSpeed > 0)
                rotationWheels.transform.localRotation = Quaternion.Euler(transform.rotation.x,rotationSpeed,transform.rotation.z);
            if(rotationSpeed < 0)
                rotationWheels.transform.localRotation = Quaternion.Euler(transform.rotation.x,rotationSpeed,transform.rotation.z);
            if(rotationSpeed == 0)
                rotationWheels.transform.localRotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
    }
}
