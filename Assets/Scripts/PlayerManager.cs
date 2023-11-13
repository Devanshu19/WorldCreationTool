using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    private void Awake()
    {
        // Maintain a single Instance. SINGLETON PATTERN.
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ResetPosition()
    {
        CharacterController characterController = GetComponent<CharacterController>();

        characterController.enabled = false;
        characterController.transform.position = Vector3.zero;
        characterController.enabled = true;
    }
}