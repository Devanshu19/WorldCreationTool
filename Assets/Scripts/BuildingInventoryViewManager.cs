using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInventoryViewManager : MonoBehaviour
{
    [SerializeField] GameObject buildingSelectionButtonPrefab;
    [SerializeField] RectTransform buildingSelectionButtonContainer;

    [SerializeField] List<Button> buildingSelectionButtons;
    [SerializeField] Color selectionTint;

    [SerializeField] BuildingInventoryChannelSO buildingInventoryChannelSO;
    [SerializeField] BuildingSelectionChannelSO buildingSelectionChannelSO;

    [Space, Header("DEBUG AREA"), TextArea]
    [SerializeField] String DEBUG_Information;

    private void Awake()
    {
        buildingSelectionButtons = new List<Button>();
        buildingInventoryChannelSO.E_PopulateSelectionButtons += PopulateBuildingSelectionButtons;
    }

    private void OnDisable()
    {
        buildingInventoryChannelSO.E_PopulateSelectionButtons -= PopulateBuildingSelectionButtons;
    }

    private List<BuildingData> PopulateBuildingSelectionButtons(List<BuildingData> allBuildingData)
    {
        for (int x = 0; x < allBuildingData.Count; x++)
        {
            GameObject newSelectionButton = Instantiate<GameObject>(buildingSelectionButtonPrefab, buildingSelectionButtonContainer);
            newSelectionButton.GetComponent<Image>().sprite = allBuildingData[x].UIImage;
            newSelectionButton.GetComponent<Button>().onClick.AddListener(() => SelectionBuildingButtonHandler(newSelectionButton));

            buildingSelectionButtons.Add(newSelectionButton.GetComponent<Button>());
            allBuildingData[x].SetButtonIdentificationID(newSelectionButton.GetInstanceID().ToString());
        }

        return allBuildingData;
    }

    private void SelectionBuildingButtonHandler(GameObject buttonObj)
    {
        foreach (Button button in buildingSelectionButtons)
        {
            if (button.gameObject.GetInstanceID() == buttonObj.GetInstanceID())
            {
                button.gameObject.GetComponent<Image>().color = selectionTint;
            }
            else button.gameObject.GetComponent<Image>().color = Color.white;
        }

        buildingSelectionChannelSO.RaiseSelectBuilding(buttonObj.GetInstanceID().ToString());
    }
}
