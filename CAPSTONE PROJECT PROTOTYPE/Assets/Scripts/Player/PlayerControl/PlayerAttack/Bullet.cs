using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _speed = 20f;
    private int _damage;
    public Rigidbody2D _rb;
    private GameObject _player;
    private Weapon _playerWeapon;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerWeapon = _player.GetComponent<Weapon>();
    }

    void Start()
    {
        _damage = _playerWeapon.WeaponDamage();
        StartCoroutine(SelfDestruct());
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

        if (_hitInfo.tag == "LowTierEnemy")
        {
            EnemyHealthSystem _enemy = _hitInfo.GetComponent<EnemyHealthSystem>();
            _enemy.TakeDamage(_damage);
        }

        if (_hitInfo.tag == "Boss")
        {
            Boss _boss = _hitInfo.GetComponent<Boss>();
            _boss.DamageBoss(_damage);
            Debug.Log("Boss");
        }

        Destroy(gameObject);
    }
}
