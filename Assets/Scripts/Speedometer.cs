using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody car;
    public Slider speedometer;
    public Text speedTxt;
    public float maxAngle;

    void FixedUpdate()
    {
        float percentage = GetSpeed(car) / 25;
        RotateSpeedNeedle(percentage * maxAngle);
        speedometer.value = percentage;
        DisplaySpeed();
    }

    void RotateSpeedNeedle(float rotation)
    {
        speedometer.transform.localRotation = Quaternion.Euler(0, 0, -rotation);
    }

    void DisplaySpeed()
    {
        float getspeed = GetSpeed(car) * 5;
        int speedometerx = (int)getspeed;
        speedTxt.text = speedometerx.ToString() + " KM/H";
    }

    float GetSpeed(Rigidbody rb)
    {
        float speed = rb.velocity.magnitude;
        return speed;
    }
}