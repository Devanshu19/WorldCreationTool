using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] Vector3 mousePosition;
    [SerializeField] Camera topLookCamera;

    [SerializeField] GameObject buildingPrefab;

    [SerializeField] Transform buildingContainer;

    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask layersToAvoid;

    [SerializeField] BuildingSelectionChannelSO buildingSelectionChannelSO;

    public void MouseLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SpawnBuilding();
        }
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePosition = new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y, 5);
    }

    public void SpawnBuilding()
    {
        Ray ray = topLookCamera.ScreenPointToRay(mousePosition);
        RaycastHit rayHitData;

        if (Physics.Raycast(ray, out rayHitData, int.MaxValue))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (rayHitData.collider.gameObject.layer == Mathf.RoundToInt(Mathf.Log(groundMask.value, 2)))
                {
                    GameObject buildingPrefab = buildingSelectionChannelSO.RaiseGetCurrentlySelectedBuilding();

                    Instantiate<GameObject>(buildingPrefab, rayHitData.point, Quaternion.identity, buildingContainer);

                    Debug.DrawLine(ray.origin, rayHitData.point, Color.red, 100);
                }
            }
        }
    }
}
