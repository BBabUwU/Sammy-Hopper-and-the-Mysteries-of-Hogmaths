using UnityEngine;
using Cinemachine;

public class BossCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCam;
    [SerializeField] private CinemachineVirtualCamera _bossCam;

    public bool _isAtBossArena = false;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(16, 10);
        Physics2D.IgnoreLayerCollision(16, 15);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isAtBossArena = true;
            ChangeCamera();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isAtBossArena = false;
            ChangeCamera();
        }
    }

    private void ChangeCamera()
    {
        if (_isAtBossArena)
        {
            _playerCam.Priority = 0;
            _bossCam.Priority = 1;
        }
        else
        {
            _playerCam.Priority = 1;
            _bossCam.Priority = 0;
        }
    }
}
