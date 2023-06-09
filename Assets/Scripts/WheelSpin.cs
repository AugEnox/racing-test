using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    public static bool reverse;
    public float brakingPower;
    public float torque;
    public float hInput;
    public float vInput;
    public float steer;
    public float maxsteer;
    public Rigidbody rb;
    public WheelCollider FLCol, FRCol, RLCol, RRCol;
    public Transform FL, FR, RL, RR, FLBR, FRBR;
    public Vector3 offset;
    public GameObject brakelights;
    public GameObject reverselights;

    public Material brakes;

    //This function accelerates the vehicle forward and backwards
    void Accelerate()
    {
        RLCol.motorTorque = torque * vInput;
        RRCol.motorTorque = torque * vInput;
    }

    // This function checks when to Accelerate
    void AccelerationCheck(Vector3 Velocity)
    {
        // If the car moves forward
        if (Velocity.z > 0.01)
        {
            
            if (vInput > 0)
            {
                reverselights.SetActive(false);
                Accelerate();
                reverse = false;
            }
            if (vInput < 0)
            {
                Brake(true);
            }
            else
            {
                Brake(false);
            }
        }
        else
        {
            // If the car reverses
            if (Velocity.z < -0.01)
            {
                
                if (vInput >= 0)
                {
                    Brake(true);
                    reverselights.SetActive(false);
                }
                if (vInput < 0)
                {
                    Accelerate();
                    
                    reverselights.SetActive(true);
                    if(Velocity.z < -3)
                        reverse = true;
                }
            }
            else
            {
                reverselights.SetActive(false);
                Brake(false);
                Accelerate();
            }
        }
    }

    // This function Brakes the vehicle
    void Brake(bool on)
    {
        if (on)
        {
            RLCol.brakeTorque = Mathf.Clamp(Mathf.Abs(RLCol.rpm) * 8f, 100, 10000);
            RRCol.brakeTorque = Mathf.Clamp(Mathf.Abs(RRCol.rpm) * 8f, 100, 10000);
            FLCol.brakeTorque = Mathf.Clamp(Mathf.Abs(FLCol.rpm) * 8f, 100, 10000);
            FRCol.brakeTorque = Mathf.Clamp(Mathf.Abs(FRCol.rpm) * 8f, 100, 10000);
            BrakeLightOn(on);
        }
        else
        {
            RLCol.brakeTorque = 0;
            RRCol.brakeTorque = 0;
            FLCol.brakeTorque = 0;
            FRCol.brakeTorque = 0;
            BrakeLightOn(on);
        }
    }

    // This function steers the vehicle left and right
    void Steer(float steer)
    {
        float steeringAngle = steer * hInput;
        FLCol.steerAngle = steeringAngle;
        FRCol.steerAngle = steeringAngle;
        FLBR.localRotation = Quaternion.Euler(0, steeringAngle, 0);
        FRBR.localRotation = Quaternion.Euler(0, steeringAngle, 0);
    }

    // This function updates the render wheels
    void UpdateWheels()
    {
        UpdateWheel(RLCol, RL);
        UpdateWheel(RRCol, RR);
        UpdateWheel(FLCol, FL);
        UpdateWheel(FRCol, FR);
        UpdateBrake(FLCol, FLBR);
        UpdateBrake(FRCol, FRBR);
    }

    // This function mirrors the transforms of a collider to the transforms of another object
    void UpdateWheel(WheelCollider wCollider, Transform wTransform)
    {
        Vector3 pos = wTransform.position;
        Quaternion quat = wTransform.rotation;

        wCollider.GetWorldPose(out pos, out quat);

        wTransform.position = pos;
        wTransform.rotation = quat;
    }

    // This function fixes the mirroring bug when the brake disks rotate around one axis while the parent object rotates
    void UpdateBrake(WheelCollider wCollider, Transform BrTransform)
    {
        Vector3 pos = BrTransform.position;
        Quaternion quat;

        wCollider.GetWorldPose(out pos, out quat);

        BrTransform.position = pos + offset;
    }

    // 
    void BrakeLightOn(bool on)
    {
        brakelights.SetActive(on);
    }

    float GetSpeed(Rigidbody rb)
    {
        float speed = rb.velocity.magnitude;
        return speed;
    }

    void Start()
    {
        reverse = false;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Checkpoints.raceIsGo)
        {
            hInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");
        }
        else
        {
            hInput = 0;
            vInput = 0;
        }

        Vector3 carVelocity = rb.transform.InverseTransformDirection(rb.velocity);

        AccelerationCheck(carVelocity);
        UpdateWheels();
        Steer(maxsteer);
    }
}