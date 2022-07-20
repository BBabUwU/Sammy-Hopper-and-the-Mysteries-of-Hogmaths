using UnityEngine;

public class TeleportToBoss : MonoBehaviour
{
    [SerializeField] private Transform _bossArenaPosition;
    [SerializeField] private Transform _player;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(17, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _player.position = _bossArenaPosition.position;
    }
}
