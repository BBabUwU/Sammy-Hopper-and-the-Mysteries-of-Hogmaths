using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _speed = 20f;
    public int _damage = 40;
    public Rigidbody2D _rb;
    public GameObject _player;

    void Start()
    {
        StartCoroutine(SelfDestruct());
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreLayerCollision(10, 13);
        Physics2D.IgnoreLayerCollision(10, 14);
        if (Input.GetKey(KeyCode.W))
        {
            _rb.velocity = transform.up * _speed;
        }
        else
        {
            _rb.velocity = transform.right * _speed;
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D _hitInfo)
    {
        EnemyHealthSystem _enemy = _hitInfo.GetComponent<EnemyHealthSystem>();
        if (_enemy != null)
        {
            _enemy.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
