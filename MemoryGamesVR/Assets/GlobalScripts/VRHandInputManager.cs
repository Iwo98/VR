using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHandInputManager : MonoBehaviour
{
    private InputDevice rightController;
    private InputDevice leftController;
    public bool rightTriggerPressed = false;
    public bool leftTriggerPressed = false;
    // Start is called before the first frame update
    /* void Start()
     {
         List<InputDevice> devices = new List<InputDevice>();
         InputDevices.GetDevices(devices);
         Debug.Log(" === Detected devices: ===");
         foreach (var device in devices)
         {
             Debug.Log(device.name + device.characteristics);
             if(device.name.Contains("Right")) {
                 rightController = device;
                 rightTriggerPressed = false;
             }
             else if (device.name.Contains("Left"))
             {
                 leftController = device;
                 leftTriggerPressed = false;
             }
         }
     }

     // Update is called once per frame
     void Update()
     {
         rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rTriggerValue);
         rightTriggerPressed = rTriggerValue;
         leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool lTriggerValue);
         leftTriggerPressed = lTriggerValue;
     }*/
    //private InputDevice _targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        TryInitializeRight();
        TryInitializeLeft();
    }

    void TryInitializeRight()
    {
        var inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, inputDevices);

        if (inputDevices.Count == 0)
        {
            return;
        }
        else
        {
            rightController = inputDevices[0];
        }
    }

    void TryInitializeLeft()
    {
        var inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, inputDevices);

        if (inputDevices.Count == 0)
        {
            return;
        }
        else
        {
            leftController = inputDevices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rightController.isValid)
        {
            TryInitializeRight();
        }
        else
        {
            rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rTriggerValue);
            rightTriggerPressed = rTriggerValue;
        }

        if (!leftController.isValid)
        {
            TryInitializeLeft();
        }
        else
        {
            leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool lTriggerValue);
            leftTriggerPressed = lTriggerValue;
        }
    }
}
