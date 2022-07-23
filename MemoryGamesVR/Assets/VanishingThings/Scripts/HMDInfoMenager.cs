using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HMDInfoMenager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(XRSettings.isDeviceActive);
        Debug.Log(XRSettings.loadedDeviceName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
