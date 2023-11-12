using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSelectionChannelSO", menuName = "WorldCreationTool/BuildingSelectionChannelSO", order = 0)]
public class BuildingSelectionChannelSO : ScriptableObject
{
    public delegate GameObject GetCurrentlySelectedBuilding();
    public event GetCurrentlySelectedBuilding E_GetCurrentlySelectedBuilding;

    public delegate void SelectBuilding(string id);
    public event SelectBuilding E_SelectBuilding;

    public GameObject RaiseGetCurrentlySelectedBuilding()
    {
        if (E_GetCurrentlySelectedBuilding == null)
        {
            Debug.LogError("GetCurrentlySelectedBuilding event was called but no one to respond to it.");
            return null;
        }
        else return E_GetCurrentlySelectedBuilding.Invoke();
    }

    public void RaiseSelectBuilding(string id)
    {
        if (E_SelectBuilding == null)
        {
            Debug.LogError("GetCurrentlySelectedBuilding event was called but no one to respond to it.");
        }
        else E_SelectBuilding.Invoke(id);
    }
}