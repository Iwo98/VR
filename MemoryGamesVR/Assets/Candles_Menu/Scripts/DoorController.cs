using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool rightSide;
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
            if (!rightSide)
                myAnimator.CrossFadeInFixedTime("DoorOpen", 0.6f);
            else
                myAnimator.CrossFadeInFixedTime("DoorOpenRight", 0.6f);
            doorOpen = true;
        }
        else
        {
            if (!rightSide)
                myAnimator.CrossFadeInFixedTime("DoorClose", 0.6f);
            else
                myAnimator.CrossFadeInFixedTime("DoorCloseRight", 0.6f);
            doorOpen = false;
        }
    }
}
