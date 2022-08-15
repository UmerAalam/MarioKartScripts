using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Variables")]
    private float currentSpeed = 0f;
    [SerializeField] float realSpeed = 1f;
    [SerializeField] float tireSpeedRotation = 0f;
    [SerializeField] float carRotationSpeed = 20f;
    [SerializeField] float maxCarRotation = 20f;
    [SerializeField] float maxTireTurn = 20f;
    private float horizontal,vertical;
    [Header("Tires")]
    [Header("All Tires")]
    [SerializeField] Transform[] allTires;
    [Header("Front Tires")]
    [SerializeField] Transform[] frontTires;
    [SerializeField] Rigidbody carRb;
    private Vector3 moveForward;
    private Vector3 leftRightTurn;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Stop and Run Car Rotation
        if (vertical > 0)
            currentSpeed = 1f;
        else if (vertical < 0)
            currentSpeed = 1f;
        else if (vertical == 0)
            currentSpeed = 0f;
        Debug.Log(currentSpeed);
    }
    void FixedUpdate()
    {
        ForwardBackward();
        LeftRightMovement();
    }
    void ForwardBackward()
    {
        moveForward = Vector3.forward * currentSpeed * realSpeed * vertical * Time.deltaTime;
        carRb.AddForce(moveForward);
    }
    void LeftRightMovement()
    {
        leftRightTurn = Vector3.up * carRotationSpeed * horizontal * vertical * Time.deltaTime;
        transform.Rotate(leftRightTurn);
    }
}
