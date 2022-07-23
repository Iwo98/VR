using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    private Vector3Int spherePoint = new Vector3Int(0, 0, 0);
    private int sphereId = 0;

    public Material inactiveMaterial, activeMaterial, neighbourMaterial, invisibleMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void setInitialValues(int id, Vector3Int point)
    {
        sphereId = id;
        spherePoint = point;
    }

    private void OnMouseEnter()
    {
        MainGameMagicDuel[] main_game = Object.FindObjectsOfType<MainGameMagicDuel>();
        main_game[0].updateMagicSign(sphereId, spherePoint);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MainGameMagicDuel[] main_game = Object.FindObjectsOfType<MainGameMagicDuel>();
            main_game[0].startDrawing(sphereId, spherePoint);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        MainGameMagicDuel main_game = Object.FindObjectsOfType<MainGameMagicDuel>()[0];
        if (main_game.isRightHand && target.tag == "RightHand")
        {
            main_game.updateMagicSign(sphereId, spherePoint);
        }
        else if (!main_game.isRightHand && target.tag == "LeftHand")
        {
            main_game.updateMagicSign(sphereId, spherePoint);
        }
    }

    void OnTriggerStay(Collider target)
    {
        MainGameMagicDuel main_game = Object.FindObjectsOfType<MainGameMagicDuel>()[0];
        if (main_game.isRightHand && target.tag == "RightHand" && main_game.isVRTriggerPressed())
        {
            main_game.startDrawing(sphereId, spherePoint);
        }
        else if (!main_game.isRightHand && target.tag == "LeftHand" && main_game.isVRTriggerPressed())
        {
            main_game.startDrawing(sphereId, spherePoint);
        }
    }

    public void setInactive()
    {
        transform.Find("SphereCore").GetComponent<MeshRenderer>().enabled = true;
        transform.Find("SphereCore").GetComponent<MeshRenderer>().material = inactiveMaterial;
    }

    public void setActive()
    {
        transform.Find("SphereCore").GetComponent<MeshRenderer>().enabled = true;
        transform.Find("SphereCore").GetComponent<MeshRenderer>().material = activeMaterial;
    }

    public void setNeighbour()
    {
        transform.Find("SphereCore").GetComponent<MeshRenderer>().enabled = true;
        transform.Find("SphereCore").GetComponent<MeshRenderer>().material = neighbourMaterial;
    }

    public void setInvisible()
    {
        transform.Find("SphereCore").GetComponent<MeshRenderer>().enabled = true;
        transform.Find("SphereCore").GetComponent<MeshRenderer>().material = inactiveMaterial;

    }

    public int getSphereId()
    {
        return sphereId;
    }

    public Vector3Int getSpherePoint()
    {
        return spherePoint;
    }

    public void destroySphere()
    {
        Destroy(gameObject);
    }
}
