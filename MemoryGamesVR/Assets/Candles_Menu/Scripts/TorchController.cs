using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR;

public class TorchController : MonoBehaviour
{
    public Material fireShape;
    public Material iceShape;
    public Material poisonShape;
    public AudioClip fireSound;
    public AudioClip iceSound;
    public AudioClip poisonSound;
    public Material fireGlow;
    public Material iceGlow;
    public Material poisonGlow;
    private int state = 0;
    private Light myLight;
    private Renderer myRenderer;
    private AudioSource audioSource;
    private ParticleSystem[] childrenParticleSytems;
    private bool grabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        myRenderer = transform.parent.GetComponent<Renderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        childrenParticleSytems = gameObject.GetComponentsInChildren<ParticleSystem>();
        SetOnFire(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabbed)
            ReturnToDropZone();
    }

    public void OnGrab()
    {
        grabbed = !grabbed;
    }

    public void ReturnToDropZone()
    {
        transform.parent.position = new Vector3(0.4f, 0.7f, -0.5f);
    }

    public void ChangeState()
    {
        switch (state)
        {
            case 0:
                SetOnIce();
                break;
            case 1:
                SetOnPoison();
                break;
            case 2:
                SetOnFire();
                break;

            default:
                break;
        }
        state++;
        if (state >= 3)
            state = 0;
    }

    public int GetState()
    {
        return state;
    }

    private void SetOnFire(bool sound = true)
    {
        if (sound)
        {
            audioSource.clip = fireSound;
            audioSource.Play();
        }
        var mats = myRenderer.materials;
        mats[1] = fireGlow;
        myRenderer.materials = mats;
        myLight.color = new Color(1, 1, 0.7f);
        Gradient fire = new Gradient();
        fire.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.red, 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        ChangeParticles(fire, fireShape);

    }

    private void SetOnIce()
    {
        var mats = myRenderer.materials;
        mats[1] = iceGlow;
        myRenderer.materials = mats;
        audioSource.clip = iceSound;
        audioSource.Play();
        myLight.color = new Color(0.3f, 0.9f, 1);
        Gradient ice = new Gradient();
        ice.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.cyan, 0.0f), new GradientColorKey(Color.blue, 0.5f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        ChangeParticles(ice, iceShape);
    }

    private void SetOnPoison()
    {
        var mats = myRenderer.materials;
        mats[1] = poisonGlow;
        myRenderer.materials = mats;
        audioSource.clip = poisonSound;
        audioSource.Play();
        myLight.color = new Color(0.3f, 1, 0.6f);
        Gradient poison = new Gradient();
        poison.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.green, 0.25f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        ChangeParticles(poison, poisonShape);
    }

    private void ChangeParticles(Gradient grad, Material shape)
    {
        foreach (ParticleSystem childPS in childrenParticleSytems)
        {
            childPS.GetComponent<Renderer>().material = shape;
            var col = childPS.colorOverLifetime;
            col.enabled = true;
            col.color = grad;
            ParticleSystem[] grandchildrenParticleSystems = childPS.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem grandchildPS in grandchildrenParticleSystems)
            {
                col = grandchildPS.colorOverLifetime;
                col.enabled = true;
                col.color = grad;
            }
        }
    }
}
