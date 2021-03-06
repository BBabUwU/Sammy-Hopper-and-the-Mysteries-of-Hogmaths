using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private PlayerHealthSystem _playerHealth;

    void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealthSystem>();
        Physics2D.IgnoreLayerCollision(10, 11);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            _playerHealth.Heal(50f);
        }
    }
}
