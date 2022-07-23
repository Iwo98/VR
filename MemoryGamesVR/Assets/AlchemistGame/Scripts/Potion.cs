using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public AudioClip glassBreak;
    public AudioClip waterSplash;
    public GameObject breakAnimation;
    public GameObject splashAnimation;
    public Transform cameraPos;

    Vector3 startPosition;
    Quaternion startRotation;
    private float posResetVal = 0.15f;
    private int potionId = 0;


    private AudioSource audioSource;

    Vector3 cauldronPosition = new Vector3(-0.6f, 0.8f, 0.9f);  // for playing with mouse input
    private float cauldronGlobalYPos = 0.35f; // for rendering splash animation

    public GameObject potionNeutralTemplate;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        int col_layer = collision.gameObject.layer;
        Vector3 diff_vec = getDifferenceVector(startPosition, gameObject.transform.position);

        if (col_layer == 7 || col_layer == 6) // Cauldron or potion
        {
            
        }
        else if (col_layer == 11) // Cauldron Liquid
        {
            ContactPoint point = collision.GetContact(0);
            if (point.thisCollider.name.Contains("Col1") && collision.collider.name.Contains("Liquid")) // Collision with liquid
            {
                GameUtils gameUtils = GameObject.FindObjectsOfType<GameUtils>()[0];
                audioSource.clip = waterSplash;
                audioSource.volume = 0.3f;
                audioSource.Play();
                Vector3 splashPos = gameObject.transform.position;
                splashPos.y = cauldronGlobalYPos;
                GameObject animation = Object.Instantiate(splashAnimation, splashPos, Quaternion.identity);
                animation.GetComponent<destroyEffect>().setAutomaticDestroy(2.0f);
                MainGameAlchemist[] main_game = Object.FindObjectsOfType<MainGameAlchemist>();
                main_game[0].checkRecipe(potionId);
                resetPosition();
            }
        }
        else if (diff_vec.x > posResetVal || diff_vec.y > posResetVal || diff_vec.z > posResetVal)
        {
            Vector3 breakPos = gameObject.transform.position;
            GameObject animation = Object.Instantiate(breakAnimation, breakPos, Quaternion.identity);
            animation.GetComponent<destroyEffect>().setAutomaticDestroy(2.0f);
            audioSource.clip = glassBreak;
            audioSource.volume = 0.3f;
            audioSource.Play();
            resetPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void resetPosition()
    {
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    Vector3 getDifferenceVector(Vector3 start, Vector3 end)
    {
        Vector3 diff_vec = (gameObject.transform.position - startPosition);
        if (diff_vec.x < 0)
        {
            diff_vec.x *= -1;
        }
        if (diff_vec.y < 0)
        {
            diff_vec.y *= -1;
        }
        if (diff_vec.z < 0)
        {
            diff_vec.z *= -1;
        }
        return diff_vec;
    }

    public void transformPotion()
    {
        Vector3 breakPos = gameObject.transform.position;
        GameObject animation = Object.Instantiate(breakAnimation, breakPos, Quaternion.identity);
        animation.GetComponent<destroyEffect>().setAutomaticDestroy(2.0f);
        var pot_temp = Object.Instantiate(potionNeutralTemplate, startPosition, startRotation);
        pot_temp.GetComponent<Potion>().setPotionId(potionId);
        Destroy(gameObject);
    }

    public void destroyPotion()
    {
        Destroy(gameObject);
    }

    public void setPotionId(int id)
    {
        potionId = id;
    }

    public int getPotionId()
    {
        return potionId;
    }

    private void OnMouseDown()
    {
        transform.position = cauldronPosition;
    }
}