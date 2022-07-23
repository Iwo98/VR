using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExitController : MonoBehaviour
{
    private Animator myAnimator;
    private bool doorOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnimation()
    {
        if (!doorOpen)
        {
            myAnimator.CrossFadeInFixedTime("DoorOpen", 0.6f);
            doorOpen = true;
        }
        else
        {
            myAnimator.CrossFadeInFixedTime("DoorClose", 0.6f);
            doorOpen = false;
        }
    }
}
