using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasRaycaster : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        raycaster = GetComponent<GraphicRaycaster>();

        //Fetch the Event System from the Scene
        eventSystem = GetComponent<EventSystem>();
    }

    public bool IsPointerOverCanvas()
    {

        //Set up the new Pointer Event
        pointerEventData = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            return true;
        }

        return false;
    }
}