using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingInventoryChannelSO", menuName = "WorldCreationTool/BuildingInventoryChannelSO", order = 0)]
public class BuildingInventoryChannelSO : ScriptableObject
{
    public delegate List<BuildingData> PopulateSelectionButtons(List<BuildingData> allBuildingData);
    public event PopulateSelectionButtons E_PopulateSelectionButtons;

    public List<BuildingData> RaisePopulateSelectionButtons(List<BuildingData> allBuildingData)
    {
        if (E_PopulateSelectionButtons == null)
        {
            Debug.LogError("PopulateSelectionButtons event was called but no one responded!");
            return new List<BuildingData>();
        }
        else return E_PopulateSelectionButtons.Invoke(allBuildingData);
    }
}