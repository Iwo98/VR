using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    private Material[] defaultColor;
    private Material selectedColor;
    private MeshRenderer myRend;
    private bool isSelected = false;
    //private bool justClicked = false;

    [SerializeField]
    public float floorOffset;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        defaultColor = myRend.materials;
        selectedColor = Resources.Load("selectedItem", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickMe()
    {
        if (myRend.enabled) {
            if (!isSelected)
            {
                Material[] tempMaterials = new Material[myRend.materials.Length];
                for (int material_id = 0; material_id < myRend.materials.Length; material_id++)
                {
                    tempMaterials[material_id] = selectedColor;
                }
                myRend.materials = tempMaterials;
                isSelected = !isSelected;
            }
            else
            {
                myRend.materials = defaultColor;
                isSelected = !isSelected;
            }
        }
    }

    public void resetMaterial()
    {
        myRend.materials = defaultColor;
    }

    public void setSelected()
    {
        isSelected = true;
    }

    public void setNotSelected()
    {
        isSelected = false;
    }

    public bool getSelected() 
    {
        return isSelected;
    }

    public void destroyItem()
    {
        Destroy(myRend.GetComponent<ClickOn>().gameObject);
    }

    public void setInvisible()
    {
        myRend.GetComponent<Renderer>().enabled = false;
    }

    public void setVisible()
    {
        myRend.GetComponent<Renderer>().enabled = true;
    }

    public void getTransform(bool isClicked)
    {
        
    }
}
