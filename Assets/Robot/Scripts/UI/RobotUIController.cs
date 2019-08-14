using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotUIController : MonoBehaviour
{
    public float current_angle0;
    public float current_angle2;
    public float desiredAngle;
    public float temp;

    private bool angleReached = true;

    public ServoMotor servo;

    [SerializeField]
    private Robot robot;
    [SerializeField]
    private Toggle toggleUpper;
    [SerializeField]
    private Toggle toggleLower;
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text headingText;
    [SerializeField]
    public ServoUIController upperLeg1Servo;
    [SerializeField]
    public ServoUIController lowerLeg1Servo;
    [SerializeField]
    public ServoUIController upperLeg2Servo;
    [SerializeField]
    public ServoUIController lowerLeg2Servo;
    [SerializeField]
    public ServoUIController upperLeg3Servo;
    [SerializeField]
    public ServoUIController lowerLeg3Servo;
    [SerializeField]
    public ServoUIController upperLeg4Servo;
    [SerializeField]
    public ServoUIController lowerLeg4Servo;
    [SerializeField]
    private ServoProfile profile;

    private bool walk_right = false;
    public bool walking = false, invert = false;

    [SerializeField]
    public int WT_MAX = 50;
    private int walk_timer;
    private int walk_elaps = 0;

    private void Awake()
    {
        upperLeg1Servo.servo = robot.legs[0].upperLeg;
        lowerLeg1Servo.servo = robot.legs[0].lowerLeg;
        upperLeg2Servo.servo = robot.legs[1].upperLeg;
        lowerLeg2Servo.servo = robot.legs[1].lowerLeg;
        upperLeg3Servo.servo = robot.legs[2].upperLeg;
        lowerLeg3Servo.servo = robot.legs[2].lowerLeg;
        upperLeg4Servo.servo = robot.legs[3].upperLeg;
        lowerLeg4Servo.servo = robot.legs[3].lowerLeg;

        toggleUpper.onValueChanged.AddListener(ToggleUpperLegs);
        toggleLower.onValueChanged.AddListener(ToggleLowerLegs);

        for (int i = 0; i < 4; i++)
        {
            //robot.legs[i].upperLeg.SetAngle(Random.Range(-90.0f, 90.0f));
        }
        
    }

    private void Update()
    {
        if (!robot) return;
        if (distanceText) distanceText.text = "Distance: " + robot.GetDistance().ToString("0.000") + "m";
        if (headingText) headingText.text = "Heading: " + robot.GetHeading().ToString("00.0") + "°";

        //StartCoroutine(SetAngleCoroutine());

        walking = Input.GetKey(KeyCode.UpArrow);

        desiredAngle = 45.0f;

        if (walking) {
            walk_timer -= 1;
            walk_elaps += 1;

            if (walk_timer <= 0) {
                walk_right = !walk_right;
                walk_elaps = 0;
                walk_timer = WT_MAX;
                invert = !invert;
            }

            invert = walk_elaps >= walk_timer - 1;

            if (walk_right) {
                robot.legs[0].upperLeg.SetAngle(-90);
                robot.legs[1].upperLeg.SetAngle(90);
                robot.legs[2].upperLeg.SetAngle(-45);
                robot.legs[3].upperLeg.SetAngle(45);

                robot.legs[0].lowerLeg.SetAngle(0);
                robot.legs[1].lowerLeg.SetAngle(45);
                robot.legs[2].lowerLeg.SetAngle(0);
                robot.legs[3].lowerLeg.SetAngle(0);
            }
            else {
                robot.legs[0].upperLeg.SetAngle(45);
                robot.legs[1].upperLeg.SetAngle(-45);
                robot.legs[2].upperLeg.SetAngle(90);
                robot.legs[3].upperLeg.SetAngle(-90);

                robot.legs[0].lowerLeg.SetAngle(0);
                robot.legs[1].lowerLeg.SetAngle(0);
                robot.legs[2].lowerLeg.SetAngle(0);
                robot.legs[3].lowerLeg.SetAngle(45);
            }
        }

        
        if (angleReached == true)
        {
            //angleReached = false;
        }
    }


    //Function to move upperLeg
    public IEnumerator moveRandomAngleCoroutine(int legIndex, float desiredAngle)
    {
        robot.legs[legIndex].upperLeg.SetAngle(desiredAngle);
        //robot.legs[1].upperLeg.SetAngle(desiredAngle);
        //robot.legs[2].upperLeg.SetAngle(desiredAngle);
        //robot.legs[3].upperLeg.SetAngle(desiredAngle);

        print("Target: " + desiredAngle);
        print("Current0: " + robot.legs[legIndex].upperLeg.GetServoAngle());
        while (true)
        {
            yield return null;
            if (Mathf.Abs(robot.legs[legIndex].upperLeg.GetServoAngle() - desiredAngle) <= 5)
            {
                angleReached = true;
                print("Current: " + robot.legs[legIndex].upperLeg.GetServoAngle());
                break;
            }
        }
    }

    //private void trial_func()
    //{
    //    //Yield return suspends routine execution for given amount if seconds using scaled time
    //    //If profile exisits use delay set there, else timescale set to 0

    //    for (int i = 0; i < 4; i++)
    //    {
    //        float target = Random.Range(-90.0f, 90.0f);
    //        robot.legs[i].upperLeg.SetAngle(target);

    //      //  current_angle0 = robot.legs[0].upperLeg.GetServoAngle();
    //      //  current_angle2 = robot.legs[2].upperLeg.GetServoAngle();

    //        if (current_angle == target)
    //        {

    //        }

    //        else
    //        {
    //            yield return new WaitForSeconds(1);
    //        }

    //        //}

    //        do robot.legs[0].upperLeg.SetAngle(-45);

    //    while (current_angle0 != -45);

    //    do robot.legs[0].upperLeg.SetAngle(45);

    //    while (current_angle0 != 45);

    //    do robot.legs[2].upperLeg.SetAngle(-45);

    //    while (current_angle2 != -45);

    //    do robot.legs[2].upperLeg.SetAngle(45);

    //    while (current_angle2 != 45);
    //}

    public void ToggleUpperLegs(bool value)
    {
        foreach (var leg in robot.legs)
        {
            leg.upperLeg.IsFixed = value;
        }
    }

    public void ToggleLowerLegs(bool value)
    {
        foreach (var leg in robot.legs)
        {
            leg.lowerLeg.IsFixed = value;
        }

    }
}
