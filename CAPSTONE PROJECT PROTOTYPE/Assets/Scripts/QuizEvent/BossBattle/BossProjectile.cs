using System.Collections;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float _projectileSpeed = 20f;
    public Rigidbody2D _rb;
    private GameObject _player;
    private PlayerHealthSystem _playerhealth;
    public bool _isFlipped = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        LookAtPlayer();
        _playerhealth = _player.GetComponent<PlayerHealthSystem>();
        _rb.velocity = -transform.right * _projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Destroy(gameObject);
            _playerhealth.Damage(10f);
        }

        if (other.collider.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }

    public void LookAtPlayer()
    {
        Vector2 _flip = transform.localScale;
        _flip.x *= -1;
        if (this.transform.position.x > _player.transform.position.x)
        {
            transform.localScale = _flip;
        }
        else if (this.transform.position.x < _player.transform.position.x)
        {
            transform.localScale = _flip;
        }
    }
}
