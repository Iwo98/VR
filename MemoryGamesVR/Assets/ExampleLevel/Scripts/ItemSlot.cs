using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject Level;
    public int SlotIndex;

    MainGameExampleLevel mainGameExampleLevel;

    void Awake()
    {
        mainGameExampleLevel = Level.GetComponent<MainGameExampleLevel>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            mainGameExampleLevel.gamesInOrder[SlotIndex] = eventData.pointerDrag.name;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("Show me the list");
            DumpArray(mainGameExampleLevel.gamesInOrder);
        }
    }

    public static void DumpToConsole(object obj)
    {
        var output = JsonUtility.ToJson(obj, true);
        Debug.Log(output);
    }

    public static void DumpArray(List<string> list)
    {
        foreach(string item in list)
        {
            Debug.Log(item);
        }
    }

}

