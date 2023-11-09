using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] Vector2 mousePosition;

    public void MouseLeft(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse Button");
        if (context.phase == InputActionPhase.Performed) Debug.Log("Button Down");
        else if (context.phase == InputActionPhase.Canceled) Debug.Log("Button Up");
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) mousePosition = context.ReadValue<Vector2>();
    }
}
