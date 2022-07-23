using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{

    private Vector3 originalPosition, originalChildPosition;
    private Quaternion originalRotation, originalChildRotation;
    private Transform originalParent;

    private float timer;
    private bool saveTime;

    public int arrowType; // Wyr�niamy 3 typy strza� przy czym dodatkowy tryb czwarty jest uniwersalny (0 - uniwersalny, 1 - czerwony, 2 - zielony, 3 - niebieski)

    private static float longOfSaveTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        try
        {
            originalChildPosition = transform.transform.position;
            originalChildRotation = transform.transform.rotation;
        }
        catch
        {
            Debug.Log("There is no children");
        }
originalParent = gameObject.transform.parent;

        timer = longOfSaveTime;
        saveTime = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (saveTime && timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            saveTime = false;
            timer = longOfSaveTime;
        }
        if (this.transform.position.y < -12f || this.transform.position.y > 12f ||
            this.transform.position.z < -12f || this.transform.position.z > 12f ||
            this.transform.position.x < -12f || this.transform.position.x > 12f)
        {
            gameObject.transform.position = originalPosition;
            gameObject.transform.rotation = originalRotation;

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameObject.transform.SetParent(originalParent);
            try
            {
                gameObject.transform.transform.position = originalChildPosition;
                gameObject.transform.transform.rotation = originalChildRotation;
            }
            catch
            {
                Debug.Log("There is no children");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.name.Contains("Target") ||
            collision.collider.name.Contains("Floor") ||
            (collision.collider.name.Contains("Arrow") && gameObject.transform.parent.name.Contains("Crossbow") && !saveTime) ||
            !collision.collider.name.Contains("box"))
        {
            gameObject.transform.position = originalPosition;
            gameObject.transform.rotation = originalRotation;

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameObject.transform.SetParent(originalParent);
            try
            {
                gameObject.transform.transform.position = originalChildPosition;
                gameObject.transform.transform.rotation = originalChildRotation;
            }
            catch
            {
                Debug.Log("There is no children");
            }
        }

        if (saveTime)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name.Contains("Crossbow"))
        {
            if (collision.transform.childCount > 0)
            {
                if (!collision.transform.GetChild(0).GetComponent<ResetPosition>().saveTime)
                {
                    gameObject.transform.SetParent(collision.transform);
                    gameObject.transform.rotation = collision.transform.rotation;
                    gameObject.transform.localPosition = new Vector3(-0.383f, -0.03f, -3.72529e-09f);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    GetComponent<Rigidbody>().useGravity = false;
                    saveTime = true;
                }
            }
            else
            {
                gameObject.transform.SetParent(collision.transform);
                gameObject.transform.rotation = collision.transform.rotation;
                gameObject.transform.localPosition = new Vector3(-0.383f, -0.03f, -3.72529e-09f);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GetComponent<Rigidbody>().useGravity = false;
                saveTime = true;
            }
        }

    }
}
