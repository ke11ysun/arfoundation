using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGyro : MonoBehaviour
{

    private float turnSpeed = 3.0f;
    private float stopSpeed = 0.1f;
    private float panDisplayTime = 2.0f;

    public Text gyroText;
    private float maxRotationRateX;
    private float maxRotationRateY;
    private float maxRotationRateZ;

    public Text acceleroText;
    private float maxAccelerationX;
    private float maxAccelerationY;
    private float maxAccelerationZ;

    public Text panText;
    public Text panDisplayTimeText;
    private float countdownTime;
    public Text countdownTimeText;
    private int nextDirection;
    System.Random rnd = new System.Random();

    
    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        gyroText.text = "[Gyro] attitude:" + Input.gyro.attitude + "\nrotationRate:" + Input.gyro.rotationRate +
            "\nmaxrRates:" + maxRotationRateX.ToString() + " " + maxRotationRateY.ToString() + " " + maxRotationRateZ.ToString();
        acceleroText.text = "[Accelero] acceleration:" + Input.acceleration;

        panDisplayTimeText.text = "[panDisplayTime]" + panDisplayTime.ToString();
        countdownTimeText.text = "[countdownTime]" + countdownTime.ToString();


        //get max rotation rate for reference
        if (Mathf.Abs(Input.gyro.rotationRate.x) > maxRotationRateX)
        {
            maxRotationRateX = Mathf.Abs(Input.gyro.rotationRate.x);
        }
        if (Mathf.Abs(Input.gyro.rotationRate.y) > maxRotationRateY)
        {
            maxRotationRateY = Mathf.Abs(Input.gyro.rotationRate.y);
        }
        if (Mathf.Abs(Input.gyro.rotationRate.z) > maxRotationRateZ)
        {
            maxRotationRateZ = Mathf.Abs(Input.gyro.rotationRate.z);
        }


        ////check if still in display time, no generation of new pan
        //if (panDisplayTime > 0)
        //{
        //    panDisplayTime -= Time.deltaTime;
        //}
        //else
        //{
        //    panText.text = "";
        //}

        ////when last pan is finished
        //if (Mathf.Abs(Input.gyro.rotationRate.z) > turnSpeed && panDisplayTime <= 0)
        //{
        //    nextDirection = rnd.Next(0, 2);
        //    if(nextDirection == 0)
        //    {
        //        panText.text = "Pan left";
        //    } else if(nextDirection == 1)
        //    {
        //        panText.text = "Pan right";
        //    }

        //    panDisplayTime = 2.0f;

        //}



        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
        }

        if (panDisplayTime > 0)
        {
            panDisplayTime -= Time.deltaTime;
        }
        else
        {
            panText.text = "[Pan]";
        }



        //last pan is finished (turn finished)
        if (Mathf.Abs(Input.gyro.rotationRate.z) > turnSpeed && countdownTime <= 0 && panDisplayTime <= 0)
        {
            // generate new pan
            nextDirection = rnd.Next(0, 2);

            //countdown for pan display
            //countdownTime = rnd.Next(1, 11) * 60; // let user walk for 1~10 min
            countdownTime = rnd.Next(5, 10); // test
        }

        // time constraint cleared (constraint to avoid overlapping pan)
        if (countdownTime <= 0 && panDisplayTime <= 0)
        {
            // simulate user stops at intersection
            //if (Mathf.Abs(Input.acceleration.z) <= stopSpeed)
            if (Mathf.Abs(Input.gyro.attitude.x) < stopSpeed)
            {
                if (nextDirection == 0)
                {
                    panText.text = "[Pan] Pan left";
                }
                else if (nextDirection == 1)
                {
                    panText.text = "[Pan] Pan right";
                }

                // pan music for 2 seconds to guide user to turn
                panDisplayTime = 2.0f;
            }
        }

        


    }
}
