using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    public float timeToLight;
    public float timeToExtinguish;
    public Material fireShape;
    public Material iceShape;
    public Material poisonShape;
    public AudioClip fireSound;
    public AudioClip iceSound;
    public AudioClip poisonSound;
    public AudioClip extinguishSound;
    private int state = 0;
    private float torchElapsed = 0f;
    private float handElapsed = 0f;
    private Light myLight;
    private bool gameStarted = false;
    private AudioSource audioSource;
    private ParticleSystem[] childrenParticleSytems;
    private CandlesGameController gameController;
    // Start is called before the first frame update
    void Awake()
    {
        myLight = GetComponent<Light>();
        audioSource = gameObject.GetComponent<AudioSource>();
        childrenParticleSytems = gameObject.GetComponentsInChildren<ParticleSystem>();
        SetOff(false);
    }

    void Start()
    {
        gameController = FindObjectOfType<CandlesGameController>();
        gameStarted = gameController.GetStatus();
    }

    void Update()
    {
        gameStarted = gameController.GetStatus();
    }

    public int GetState()
    {
        return state;
    }

    void OnTriggerStay(Collider target)
    {
        if (target.tag == "Hand" && gameStarted)
        {
            handElapsed += Time.deltaTime;
        }

        if (target.tag == "Torch" && gameStarted)
        {
            torchElapsed += Time.deltaTime;

            if (torchElapsed > timeToLight && state == 0)
            {
                if (target.GetComponent<TorchController>().GetState() == 0)
                    SetOnFire();
                else if (target.GetComponent<TorchController>().GetState() == 1)
                    SetOnIce();
                else if (target.GetComponent<TorchController>().GetState() == 2)
                    SetOnPoison();
                torchElapsed = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Torch" && gameStarted)
        {
            torchElapsed = 0;
        }

        if (other.tag == "Hand")
        {
            if (handElapsed < timeToExtinguish && state != 0 && gameStarted)
            {
                SetOff();
            }
            handElapsed = 0;
        }
    }

    public void SetOnFire()
    {
        state = 1;
        audioSource.clip = fireSound;
        audioSource.Play();
        myLight.enabled = true;
        Gradient fire = new Gradient();
        fire.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.red, 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        EnableParticles(fire, fireShape);
        
    }

    public void SetOnIce()
    {
        state = 2;
        audioSource.clip = iceSound;
        audioSource.Play();
        myLight.enabled = true;
        Gradient ice = new Gradient();
        ice.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.cyan, 0.0f), new GradientColorKey(Color.blue, 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        EnableParticles(ice, iceShape);
    }

    public void SetOnPoison()
    {
        state = 3;
        audioSource.clip = poisonSound;
        audioSource.Play();
        myLight.enabled = true;
        Gradient poison = new Gradient();
        poison.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.green, 0.25f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        EnableParticles(poison, poisonShape);
    }

    public void SetOff(bool sound = true)
    {
        state = 0;
        if (sound)
        {
            audioSource.clip = extinguishSound;
            audioSource.Play();
        }
        myLight.enabled = false;
        DisableParticles();
    }

    private void DisableParticles()
    {
        foreach (ParticleSystem childPS in childrenParticleSytems)
        {
            ParticleSystem.EmissionModule childPSEmissionModule = childPS.emission;
            childPSEmissionModule.enabled = false;
            ParticleSystem[] grandchildrenParticleSystems = childPS.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem grandchildPS in grandchildrenParticleSystems)
            {
                ParticleSystem.EmissionModule grandchildPSEmissionModule = grandchildPS.emission;
                grandchildPSEmissionModule.enabled = false;
            }
        }
    }

    private void EnableParticles(Gradient grad, Material shape)
    {
        foreach (ParticleSystem childPS in childrenParticleSytems)
        {
            ParticleSystem.EmissionModule childPSEmissionModule = childPS.emission;
            childPSEmissionModule.enabled = true;
            childPS.GetComponent<Renderer>().material = shape;
            var col = childPS.colorOverLifetime;
            col.enabled = true;
            col.color = grad;
            ParticleSystem[] grandchildrenParticleSystems = childPS.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem grandchildPS in grandchildrenParticleSystems)
            {
                ParticleSystem.EmissionModule grandchildPSEmissionModule = grandchildPS.emission;
                grandchildPSEmissionModule.enabled = true;
                col = grandchildPS.colorOverLifetime;
                col.enabled = true;
                col.color = grad;
            }
        }
    }
}
