using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingData
{
    public Sprite UIImage;
    public GameObject buildingObject;

    private string buttonIdentificationID;

    public void SetButtonIdentificationID(string value)
    {
        buttonIdentificationID = value;
    }

    public string GetButtonIdentificationID()
    {
        return buttonIdentificationID;
    }
}

public class BuildingSelectionManager : MonoBehaviour
{
    [SerializeField] List<BuildingData> allBuildingData;

    [SerializeField] GameObject currentlySelectedBuilding;

    [SerializeField] BuildingSelectionChannelSO buildingSelectionChannelSO;
    [SerializeField] BuildingInventoryChannelSO buildingInventoryChannelSO;

    private void Awake()
    {
        // Select the first building data as the default.
        currentlySelectedBuilding = allBuildingData[0].buildingObject;
    }

    private void Start()
    {
        // Returns the same building data but with UI element ID included.
        allBuildingData = buildingInventoryChannelSO.RaisePopulateSelectionButtons(allBuildingData);
    }

    private void OnEnable()
    {
        // Subscribe to useful events.
        buildingSelectionChannelSO.E_GetCurrentlySelectedBuilding += getCurrentlySelectedBuilding;
        buildingSelectionChannelSO.E_SelectBuilding += SelectBuilding;
    }

    private void OnDisable()
    {
        // Unsubscribe from subscribed events.
        buildingSelectionChannelSO.E_GetCurrentlySelectedBuilding -= getCurrentlySelectedBuilding;
        buildingSelectionChannelSO.E_SelectBuilding -= SelectBuilding;
    }

    private GameObject getCurrentlySelectedBuilding()
    {
        return currentlySelectedBuilding;
    }

    // Selects the building for spawning.
    private void SelectBuilding(string id)
    {
        foreach (BuildingData buildingData in allBuildingData)
        {
            if (buildingData.GetButtonIdentificationID() == id)
            {
                currentlySelectedBuilding = buildingData.buildingObject;
            }
        }
    }
}
