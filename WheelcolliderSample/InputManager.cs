using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }


    public float throttleInput { get; private set; }  //used in CarController
    public float steerInput { get; private set; } //used in CarController
    public float tiltInput { get; private set; } //used in CarController
    public float reverseInput { get; private set; } //used in CarController
    public float brakeInput { get; private set; } //used in CarController
    public bool switchNext { get; private set; }  //used in GameManager
    public bool switchPrev { get; private set; } //used in GameManager
    public bool reset { get; private set; } //used in GameManager
    public bool switchCam { get; private set; } //used in GameManager

    private void Update()
    {
        steerInput = Input.GetAxis("Horizontal");
        tiltInput = Input.GetAxis("Vertical");
        throttleInput = Input.GetAxis("ThrottleInput"); //custom input, needs setting up
        reverseInput = Input.GetAxis("ReverseInput"); //custom input, needs setting up
        brakeInput = (Input.GetKey("space") || Input.GetKey(Key.XB_A) ? 1 : 0);
        switchCam = (Input.GetKeyDown("c") || Input.GetKeyDown(Key.XB_RJoyButton) ? true : false);

        switchNext = Input.GetKeyDown(KeyCode.PageUp);
        switchPrev = Input.GetKeyDown(KeyCode.PageDown);
        reset = Input.GetKeyDown(KeyCode.R);
    }
}

/// <summary>
/// This class contains mapped out keycodes for xbox and joy-con controllers.
/// It also contains keycodes for testing.
/// </summary>
class Key
{
    //test buttons
    public static KeyCode JoystickButton0 = KeyCode.JoystickButton0;
    public static KeyCode JoystickButton1 = KeyCode.JoystickButton1;
    public static KeyCode JoystickButton2 = KeyCode.JoystickButton2;
    public static KeyCode JoystickButton3 = KeyCode.JoystickButton3;
    public static KeyCode JoystickButton4 = KeyCode.JoystickButton4;
    public static KeyCode JoystickButton5 = KeyCode.JoystickButton5;
    public static KeyCode JoystickButton6 = KeyCode.JoystickButton6;
    public static KeyCode JoystickButton7 = KeyCode.JoystickButton7;
    public static KeyCode JoystickButton8 = KeyCode.JoystickButton8;
    public static KeyCode JoystickButton9 = KeyCode.JoystickButton9;
    public static KeyCode JoystickButton10 = KeyCode.JoystickButton10;
    public static KeyCode JoystickButton11 = KeyCode.JoystickButton11;
    public static KeyCode JoystickButton12 = KeyCode.JoystickButton12;
    public static KeyCode JoystickButton13 = KeyCode.JoystickButton13;
    public static KeyCode JoystickButton14 = KeyCode.JoystickButton14;
    public static KeyCode JoystickButton15 = KeyCode.JoystickButton15;
    public static KeyCode JoystickButton16 = KeyCode.JoystickButton16;
    public static KeyCode JoystickButton17 = KeyCode.JoystickButton17;
    public static KeyCode JoystickButton18 = KeyCode.JoystickButton18;
    public static KeyCode JoystickButton19 = KeyCode.JoystickButton19;
    //the axises are needed to be set up in the input manager
    public static string axis1 = "1st axis";
    public static string axis2 = "2nd axis";
    public static string axis3 = "3rd axis";
    public static string axis4 = "4th axis";
    public static string axis5 = "5th axis";
    public static string axis6 = "6th axis";
    public static string axis7 = "7th axis";
    public static string axis8 = "8th axis";
    public static string axis9 = "9th axis";
    public static string axis10 = "10th axis";
    public static string axis11 = "11th axis";
    public static string axis12 = "12th axis";
    public static string axis13 = "13th axis";
    public static string axis14 = "14th axis";
    public static string axis15 = "15th axis";
    public static string axis16 = "16th axis";
    public static string axis17 = "17th axis";
    public static string axis18 = "18th axis";
    public static string axis19 = "19th axis";
    public static string axis20 = "20th axis";

    //xbox controller
    public static KeyCode XB_A = KeyCode.JoystickButton0;
    public static KeyCode XB_B = KeyCode.JoystickButton1;
    public static KeyCode XB_X = KeyCode.JoystickButton2;
    public static KeyCode XB_Y = KeyCode.JoystickButton3;
    public static KeyCode XB_LB = KeyCode.JoystickButton4;
    public static KeyCode XB_RB = KeyCode.JoystickButton5;
    public static KeyCode XB_Select = KeyCode.JoystickButton6;
    public static KeyCode XB_Menu = KeyCode.JoystickButton7;
    public static KeyCode XB_LJoyButton = KeyCode.JoystickButton8;
    public static KeyCode XB_RJoyButton = KeyCode.JoystickButton9;
    public static string XB_LJ_X = "1st axis";
    public static string XB_LJ_Y = "2nd axis";
    public static string XB_RJ_X = "4th axis";
    public static string XB_RJ_Y = "5th axis";
    public static string XB_DP_X = "6th axis";
    public static string XB_DP_Y = "7th axis";
    public static string XB_LT = "9th axis";
    public static string XB_RT = "10th axis";
    public static string XB_Triggers = "3rd axis";

    //Joy-Cons
    public static KeyCode JoyCon_Down = KeyCode.JoystickButton0;
    public static KeyCode JoyCon_Right = KeyCode.JoystickButton1;
    public static KeyCode JoyCon_Left = KeyCode.JoystickButton2;
    public static KeyCode JoyCon_Up = KeyCode.JoystickButton3;
    public static KeyCode JoyCon_SL = KeyCode.JoystickButton4;
    public static KeyCode JoyCon_SR = KeyCode.JoystickButton5;
    public static string JoyCon_StickXaxis = "9th axis";
    public static string JoyCon_StickYaxis = "10th axis";
    public static KeyCode JoyConL_Minus = KeyCode.JoystickButton8;
    public static KeyCode JoyConR_Plus = KeyCode.JoystickButton9;
    public static KeyCode JoyConL_StickButton = KeyCode.JoystickButton10;
    public static KeyCode JoyConR_StickButton = KeyCode.JoystickButton11;
    public static KeyCode JoyConR_Home = KeyCode.JoystickButton12;
    public static KeyCode JoyConL_Capture = KeyCode.JoystickButton13;
    public static KeyCode JoyConL_L = KeyCode.JoystickButton14;
    public static KeyCode JoyConR_R = KeyCode.JoystickButton14;
    public static KeyCode JoyConL_ZL = KeyCode.JoystickButton15;
    public static KeyCode JoyConR_ZR = KeyCode.JoystickButton15;
}
