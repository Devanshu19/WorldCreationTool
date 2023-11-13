using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform playerObject;
    [SerializeField] Vector3 resetPosition;

    public void ResetPlayer()
    {
        playerObject.position = resetPosition;
    }
}
