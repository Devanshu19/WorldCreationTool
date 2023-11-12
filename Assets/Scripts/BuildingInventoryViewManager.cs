using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInventoryViewManager : MonoBehaviour
{
    [SerializeField] GameObject buildingSelectionButtonPrefab;
    [SerializeField] RectTransform buildingSelectionButtonContainer;

    [SerializeField] BuildingInventoryChannelSO buildingInventoryChannelSO;
    [SerializeField] BuildingSelectionChannelSO buildingSelectionChannelSO;

    private void OnEnable()
    {
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

            allBuildingData[x].SetButtonIdentificationID(newSelectionButton.GetInstanceID().ToString());
        }

        return allBuildingData;
    }

    private void SelectionBuildingButtonHandler(GameObject buttonObj)
    {
        buildingSelectionChannelSO.RaiseSelectBuilding(buttonObj.GetInstanceID().ToString());
    }
}
