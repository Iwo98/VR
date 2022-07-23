using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    List<Dictionary<string, float>> allCoords = new List<Dictionary<string, float>>();
    public List<ClickOn> clickOns;
    readonly System.Random rnd = new System.Random();

    [SerializeField]
    private LayerMask clickablesLayer;


    // Awake is called before the Start
    void Awake()
    {
        GeneratePositionsDictionary();
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<GameCycle>().areNewItemsShown)
            itemPicking();

    }

    public int GetAmountOfItems()
    {
        return clickOns.Count;
    }

    void itemPicking() {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            foreach (ClickOn clickItem in clickOns)
                //check if clickable element hit
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 100, clickablesLayer))
                {
                    if (clickItem != null)
                    {
                        if (rayHit.collider.GetComponent<ClickOn>() != clickItem.GetComponent<ClickOn>())
                        {
                            //reset every other to original material and selection state
                            clickItem.GetComponent<ClickOn>().resetMaterial();
                            clickItem.GetComponent<ClickOn>().setNotSelected();
                        }
                        else
                            //do proper action to clicked object
                            rayHit.collider.GetComponent<ClickOn>().ClickMe();
                    }

                }
                else
                //reset all clickable items
                    if (clickItem != null)
                {
                    clickItem.GetComponent<ClickOn>().resetMaterial();
                    clickItem.GetComponent<ClickOn>().setNotSelected();
                }
        }

        //remove item
        if (Input.GetKeyDown("r") && !Input.GetKey(KeyCode.LeftControl))
            foreach (ClickOn clickItem in clickOns)
                if (clickItem != null)
                    if (clickItem.GetComponent<ClickOn>().getSelected())
                        clickItem.GetComponent<ClickOn>().destroyItem();
    }


    void GeneratePositionsDictionary()
    {
        List<float> y_coords = new List<float>();
        y_coords.Add(0.47f);
        //y_coords.Add(1.902f);
        y_coords.Add(2.715f);
        y_coords.Add(3.654f);
        y_coords.Add(4.359f);
        //y_coords.Add(5.081f);
        foreach (float y_coord in y_coords)
        {   //column 1
            Dictionary<string, float> tempDict = new Dictionary<string, float>();
            tempDict.Add("x", -3.631f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -5.281f);
            tempDict.Add("rot_y", 180);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 2
            tempDict.Add("x", -5.484f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -5.281f);
            tempDict.Add("rot_y", 180);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 3
            tempDict.Add("x", -7.294f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -3.311f);
            tempDict.Add("rot_y", 270);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 4
            tempDict.Add("x", -7.294f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -1.407f);
            tempDict.Add("rot_y", 270);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 5
            tempDict.Add("x", -7.294f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", 1.321f);
            tempDict.Add("rot_y", 270);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 6
            tempDict.Add("x", -7.294f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", 3.172f);
            tempDict.Add("rot_y", 270);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 7
            tempDict.Add("x", -1.204f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", 5.264f);
            tempDict.Add("rot_y", 0);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 8
            tempDict.Add("x", 0.641f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", 5.264f);
            tempDict.Add("rot_y", 0);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 9
            tempDict.Add("x", 7.083f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", 3.192f);
            tempDict.Add("rot_y", 90);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 10
            tempDict.Add("x", 7.083f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", 1.334f);
            tempDict.Add("rot_y", 90);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 11
            tempDict.Add("x", 7.083f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -1.434f);
            tempDict.Add("rot_y", 90);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 12
            tempDict.Add("x", 7.083f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -3.286f);
            tempDict.Add("rot_y", 90);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 13
            tempDict.Add("x", 5.578f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -5.281f);
            tempDict.Add("rot_y", 180);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
            //column 14
            tempDict.Add("x", 3.739f);
            tempDict.Add("y", y_coord);
            tempDict.Add("z", -5.281f);
            tempDict.Add("rot_y", 180);
            allCoords.Add(new Dictionary<string, float>(tempDict));
            tempDict.Clear();
        }
    }

    public void ChoseItemsForGame(int gameLevel) {
        List<ClickOn> tempClickOns = new List<ClickOn>();
        tempClickOns.AddRange((ClickOn[])GameObject.FindObjectsOfType(typeof(ClickOn)));

        for (int i = 0; i < (gameLevel + 1) * 3; i++)
        {
            int randomID = rnd.Next(tempClickOns.Count);
            ClickOn tempClickOn = tempClickOns[randomID];
            tempClickOns.RemoveAt(randomID);
            clickOns.Add(tempClickOn);
            tempClickOn.gameObject.layer = 3;
        }
    }

    public void PlaceItemsOnShelves(int gameLevel)
    {
        List<Dictionary<string, float>> possibleCoords = AllowedPositionsBasedOnLevel(gameLevel);
        foreach (ClickOn clickItem in clickOns)
        {
            int randomID = rnd.Next(possibleCoords.Count);
            Dictionary<string, float> randomCoord = possibleCoords[randomID];
            clickItem.gameObject.transform.Translate(new Vector3(-clickItem.gameObject.transform.position.x + randomCoord["x"],
                                                                    -clickItem.gameObject.transform.position.y + randomCoord["y"] + clickItem.floorOffset,
                                                                    -clickItem.gameObject.transform.position.z + randomCoord["z"]),
                                                        Space.World);
            clickItem.gameObject.transform.Rotate(new Vector3(clickItem.gameObject.transform.rotation.x,
                                                                clickItem.gameObject.transform.rotation.y + randomCoord["rot_y"],
                                                                clickItem.gameObject.transform.rotation.z),
                                                                Space.World);
            possibleCoords.RemoveAt(randomID);
        }
    }

    List<Dictionary<string, float>> AllowedPositionsBasedOnLevel(int gameLevel)
        {
            List<Dictionary<string, float>> possibleCoords = new List<Dictionary<string, float>>();
            for (int i = 0; i < allCoords.Count; i++)
                if (i % 14 < 2 * gameLevel)
                    possibleCoords.Add(allCoords[i]);
            return possibleCoords;
        }
}
