using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class LocomotionController : MonoBehaviour
{
    public XRController right;
    public InputHelpers.Button teleportActivationButton;
    public GameObject teleportReticel;
    public float activation = 0.1f;
   

    public bool checkIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activation);
        return isActivated;
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            right.gameObject.SetActive(checkIfActivated(right));
            teleportReticel.SetActive(checkIfActivated(right));
        }
    }
}
