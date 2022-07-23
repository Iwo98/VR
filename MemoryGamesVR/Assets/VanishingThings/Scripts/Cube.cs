using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject currentGameObject;
    private Behaviour halo;
    private float oldAlpha = 1;
    public int step = 0;
    public int direction = 0;
    public float speed = 1f;
    void Start()
    {
        currentGameObject = gameObject;
        Behaviour halo = (Behaviour)currentGameObject.GetComponent("Halo");
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0)
            transform.position -= Time.deltaTime * transform.forward * speed;
        else if(direction == 1)
            transform.position -= Time.deltaTime * transform.right * speed;
        else if (direction == 2)
            transform.position += Time.deltaTime * transform.forward * speed;
        else if (direction == 3)
            transform.position += Time.deltaTime * transform.right * speed;

/*        SerializedObject halo = new SerializedObject(GetComponent("Halo"));
        if (step == 400) 
        {
            ChangeAlpha(currentGameObject.GetComponent<Renderer>().material, 0f);
            halo.FindProperty("m_Size").floatValue = 0;
            halo.ApplyModifiedProperties();
        }
        else if ( step > 150 && step < 400) 
        {
            ChangeAlpha(currentGameObject.GetComponent<Renderer>().material, 0.99f);
            halo.FindProperty("m_Size").floatValue *= 0.99f; 
            halo.ApplyModifiedProperties();
        }
        if (step > 700)
        {
            ScoreScript.scoreDown();
            Destroy(currentGameObject); 
        }
        step++;*/
        if (GameManager.game_state == 2) Destroy(currentGameObject);
    }

    void ChangeAlpha(Material mat, float alphaMul)
    {
        Color oldColor = mat.color;
        oldAlpha = oldAlpha * alphaMul;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a*alphaMul);
        mat.SetColor("_Color", newColor);
        mat.color = newColor;
    }
    public void setDirection(int dir)
    {
        direction = dir;
    }
    public void setSpeed(float s)
    {
        speed = s;
    }
}
