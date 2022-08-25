using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.RzutyFigur.Custom.Old
{
    [RequireComponent(typeof(Animator))]
    public class Hand : MonoBehaviour
    {
        public float speed;

        Animator animator;
        private float gripTarget;
        private float triggerTarget;
        private float gripCurrent;
        private float triggerCurrent;
        private string AnimatorGripParam = "Grip";
        private string AnimatorTriggerParam = "Trigger";

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            AnimateHand();
        }

        internal void SetGrip(float v)
        {
            gripTarget = v;
        }

        internal void SetTrigger(float v)
        {
            triggerTarget = v;
        }

        void AnimateHand()
        {
            if (gripCurrent != gripTarget)
            {
                gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
                animator.SetFloat(AnimatorGripParam, gripCurrent);
            }
            if (triggerCurrent != triggerTarget)
            {
                triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
                animator.SetFloat(AnimatorTriggerParam, triggerCurrent);
            }
        }
    }
}