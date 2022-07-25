using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform _firePoint;
    public GameObject _bulletPrefab;
    [SerializeField] public float _cooldownTime = 2f;
    private float _nextFireTime = 0;
    [SerializeField] private int _weaponDamage;

    public bool _isAbleToShoot;

    private void Start()
    {
        EnableWeapon();
    }

    void Update()
    {
        if (_isAbleToShoot)
        {
            Fire();
        }
    }

    private void Fire()
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

    private void OnEnable()
    {
        PlayerManager._onPlayerDeath += DisableWeapon;
        PlayerManager._disableControls += DisableWeapon;
        PlayerManager._enableControls += EnableWeapon;
    }

    private void OnDisable()
    {
        PlayerManager._onPlayerDeath -= DisableWeapon;
        PlayerManager._disableControls -= DisableWeapon;
        PlayerManager._enableControls -= EnableWeapon;
    }

    private void EnableWeapon()
    {
        _isAbleToShoot = true;
    }

    private void DisableWeapon()
    {
        _isAbleToShoot = false;
    }
}
