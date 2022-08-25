using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllersPrefabs;
    public GameObject handPrefab;
    public bool showController;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHand;

    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHand.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHand.SetActive(true);
                spawnedController.SetActive(false);
                UpdateAnimation();
            }
        }
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllersPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
                spawnedController = Instantiate(prefab, transform);
            else
            {
                spawnedController = Instantiate(controllersPrefabs[0], transform);
                Debug.Log("Did not found corresponding controller");
            }

            if (handPrefab)
            {
                spawnedHand = Instantiate(handPrefab, transform);
                handAnimator = spawnedHand.GetComponent<Animator>();
            }
        }
    }

    void UpdateAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0)
            handAnimator.SetFloat("Trigger", triggerValue);
        else
            handAnimator.SetFloat("Trigger", 0);

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0)
            handAnimator.SetFloat("Grip", gripValue);
        else
            handAnimator.SetFloat("Grip", 0);
    }
}
