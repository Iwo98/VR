using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.BalonySejfSkrzynieSiatkaTerenu.Hubert.Scripts
{
    public class Clock : MonoBehaviour
    {
        float timeLeft = 30;
        const float secondsToDegrees = -6f;
        [SerializeField]
        Transform secondsPivot = default;

        public bool active = true;
        public bool played = false;
        public AudioSource audioSource;
        public AudioClip clip;
        public float volume = 0.3f;

        void Update()
        {
            audioSource.clip = clip;
            if (active)
            {
                timeLeft -= Time.deltaTime;
                secondsPivot.localRotation = Quaternion.Euler(secondsToDegrees * timeLeft, 0f, 0f);
                if (timeLeft < 0)
                {
                    if (!played)
                    {
                        audioSource.PlayOneShot(audioSource.clip, volume);
                        played = true;
                    }
                }
            }
        }
    }
}