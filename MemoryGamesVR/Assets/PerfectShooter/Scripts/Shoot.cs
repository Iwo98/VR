using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.XR;

public class Shoot : MonoBehaviour
{
    public InputActionReference shootReference = null;

    private void Start()
    {

    }

    private void Awake()
    {
        shootReference.action.started += ShootArrow;
    }
    // Start is called before the first frame update
    private void onDestroy()
    {
       shootReference.action.started -= ShootArrow;
    }

    private void ShootArrow(InputAction.CallbackContext context)
    {
        Debug.Log("Uwaga strzelam");
        if (gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Rigidbody>().velocity = Vector3.Scale(transform.parent.transform.forward, new Vector3(18.0f, 18.0f, 18.0f));
            gameObject.transform.GetChild(0).SetParent(null);
        }
    }
}
