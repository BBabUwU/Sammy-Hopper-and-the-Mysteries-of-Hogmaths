using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject _blockedPathObj;
    public GameObject _QnAPanel;
    BlockedPath _blockedPathScript;
    void Start()
    {
        _blockedPathScript = _blockedPathObj.GetComponent<BlockedPath>();
    }

    public void EnablePlayerMovement()
    {
        _QnAPanel.SetActive(false);
        _blockedPathScript.SetPlayerControl(true);
    }
}
