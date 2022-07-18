using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject _player;
    private PlayerHealthSystem _playerHealth;
    public Transform _firePoint;
    public GameObject _projectilePrefab;
    public BossHealthbar _bossHealthBar;
    public bool _isFlipped = false;

    //Boss health
    public int _bossMaxHealth = 5000;
    public int _currentHealth;
    public bool _isDefeated = false;

    //Enemy attack
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _range;
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    //Damage 
    public GameObject _bossBattleManager;
    private BossPhase1 _bossPhase1;
    [SerializeField] private int _bossDamage = 10;



    private void Start()
    {
        _playerHealth = _player.GetComponent<PlayerHealthSystem>();
        _bossPhase1 = _bossBattleManager.GetComponent<BossPhase1>();
        _currentHealth = _bossMaxHealth;
        _bossHealthBar.SetMaxHealth(_bossMaxHealth);
    }

    public void DamagePlayer()
    {
        if (PlayerInSight()) _playerHealth.Damage(_bossDamage);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D _hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range, new Vector2(_boxCollider.bounds.size.x * _width, _boxCollider.bounds.size.y * _height), 0, Vector2.left, 0, _playerLayer);

        return _hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range, new Vector2(_boxCollider.bounds.size.x * _width, _boxCollider.bounds.size.y * _height));
    }

    public void DamageBoss(int _damage)
    {
        _damage *= _bossPhase1._damageBonusMultiplier;
        _currentHealth -= _damage;
        BossIsDead();
        _bossHealthBar.SetHealth(_currentHealth);
    }

    private void BossIsDead()
    {
        if (_currentHealth <= 0)
        {
            _isDefeated = true;
            Debug.Log("Dead");
        }
    }
    private void DestroyBoss()
    {
        Destroy(gameObject);
    }

    public void BossShoot()
    {
        Instantiate(_projectilePrefab, _firePoint.transform.position, _firePoint.rotation);
    }

    public void LookAtPlayer()
    {
        Vector3 _flipped = transform.localScale;
        _flipped.z *= -1f;

        if (transform.position.x > _player.transform.position.x && _isFlipped)
        {
            transform.localScale = _flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = false;
        }
        else if (transform.position.x < _player.transform.position.x && !_isFlipped)
        {
            transform.localScale = _flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = true;
        }
    }
}
