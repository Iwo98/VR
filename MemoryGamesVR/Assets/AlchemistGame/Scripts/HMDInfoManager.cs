using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!XRSettings.isDeviceActive)
        {
            Debug.Log("No Headset plugged");
        }
        else if(XRSettings.loadedDeviceName.Contains("Mock"))
        {
            Debug.Log("Using MockHMD");
        }
        else
        {
            Debug.Log("Device Name is: " + XRSettings.loadedDeviceName);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
