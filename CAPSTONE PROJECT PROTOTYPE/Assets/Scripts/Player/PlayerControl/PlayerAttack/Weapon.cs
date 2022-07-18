using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform _firePoint;
    public GameObject _bulletPrefab;
    [SerializeField] public float _cooldownTime = 2f;
    private float _nextFireTime = 0;
    [SerializeField] private int _weaponDamage;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _cooldownTime;
        }
    }

    public int WeaponDamage()
    {
        return _weaponDamage;
    }

    void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
