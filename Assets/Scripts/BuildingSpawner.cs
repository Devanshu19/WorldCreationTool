using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] Vector2 mousePosition;
    [SerializeField] Camera topLookCamera;

    [SerializeField] GameObject buildingPrefab;
    [SerializeField] LayerMask buildingMask;

    [SerializeField] Transform buildingContainer;

    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask layersToAvoid;

    [SerializeField] AudioClip placementSuccessfulEffect;
    [SerializeField] AudioClip placementFailedEffect;

    [SerializeField] BuildingSelectionChannelSO buildingSelectionChannelSO;

    private RaycastHit rayHitData;
    private AudioSource placementAudioPlayer;

    private void Awake()
    {
        placementAudioPlayer = GetComponent<AudioSource>();
    }

    public void MouseLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SpawnBuilding();
        }
    }

    public void SpawnBuilding()
    {
#if UNITY_ANDROID
        mousePosition = Touchscreen.current.primaryTouch.position.ReadValue();
#else
        mousePosition = Mouse.current.position.ReadValue();
#endif

        Ray ray = topLookCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out rayHitData, int.MaxValue))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                GameObject buildingPrefab = buildingSelectionChannelSO.RaiseGetCurrentlySelectedBuilding();
                GameObject newBuilding = Instantiate(buildingPrefab, rayHitData.point, Quaternion.identity, buildingContainer);
                Bounds newBuildingBounds = newBuilding.GetComponent<Collider>().bounds;
                newBuilding.SetActive(false);

                Collider[] touchingColliders = Physics.OverlapBox(rayHitData.point + Vector3.up * newBuildingBounds.size.y * 0.5f, newBuildingBounds.size * 0.5f, Quaternion.identity, buildingMask);

                if (touchingColliders.Length > 0)
                {
                    Destroy(newBuilding);
                    placementAudioPlayer.PlayOneShot(placementFailedEffect);
                }
                else
                {
                    newBuilding.SetActive(true);
                    placementAudioPlayer.PlayOneShot(placementSuccessfulEffect);
                }
            }
        }
    }
}
