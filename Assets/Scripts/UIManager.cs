using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ResetPlayer()
    {
        PlayerManager.Instance.ResetPosition();
    }
}
