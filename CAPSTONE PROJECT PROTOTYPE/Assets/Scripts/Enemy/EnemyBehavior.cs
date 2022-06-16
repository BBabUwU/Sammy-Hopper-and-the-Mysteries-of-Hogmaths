using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] public float _speed = 5f;
    [SerializeField] public float _distance = 2f;
    [SerializeField] public float _wallDistance = 2f;
    [SerializeField] public float _agroRange = 5f;
    [SerializeField] public float _attackRange = 1f;
    private bool _movingRight = true;
    private bool _isMoving;
    Rigidbody2D _rb;
    Transform _player;
    public Transform _groundDetection;
    public Transform _wallDetection;

    [Header("Player Attack")]
    [SerializeField] private float _attackCoolDown;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;
    [SerializeField] private float _damage;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;
    private float _cooldownTimer = Mathf.Infinity;
    private Animator _anim;
    private PlayerHealthSystem _playerHealth;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player").transform;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _cooldownTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        _anim.SetBool("isMoving", _isMoving);

        float _distToPlayer = Vector2.Distance(transform.position, _player.position);
        AttackPlayer();
        if (_distToPlayer < _agroRange && GroundDetect())
        {
            if (_distToPlayer < _attackRange)
            {
                _isMoving = false;
                _rb.velocity = new Vector2(0, 0);
            }
            else
            {
                ChasePlayer();
            }
        }
        else if (_distToPlayer < _agroRange && !GroundDetect())
        {
            _isMoving = false;
            _rb.velocity = new Vector2(0, 0);
        }
        else
        {
            Patrol();
        }
    }

    public bool GroundDetect()
    {
        RaycastHit2D _groundDetect = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distance, LayerMask.GetMask("Ground"));
        return _groundDetect.collider;
    }

    public bool WallDetect()
    {
        RaycastHit2D _wallDetect = Physics2D.Raycast(_wallDetection.position, _wallDetection.right, _wallDistance, LayerMask.GetMask("Ground") | LayerMask.GetMask("Wall"));
        return _wallDetect.collider;
    }

    public void Patrol()
    {

        _isMoving = true;

        if (GroundDetect())
        {
            if (_movingRight)
            {
                _rb.velocity = new Vector2(_speed, 0);
                transform.eulerAngles = new Vector3(0, 0, 0);

            }
            else
            {
                _rb.velocity = new Vector2(-_speed, 0);
                transform.eulerAngles = new Vector3(0, -180, 0);

            }
        }

        if (!GroundDetect() || WallDetect())
        {
            if (_movingRight)
            {
                _movingRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
                _rb.velocity = new Vector2(-_speed, 0);
            }
            else
            {
                _movingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
                _rb.velocity = new Vector2(_speed, 0);
            }
        }

    }

    public void ChasePlayer()
    {
        _isMoving = true;

        if (transform.position.x < _player.position.x)
        {
            _rb.velocity = new Vector2(_speed, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x > _player.position.x)
        {
            _rb.velocity = new Vector2(-_speed, 0);
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    public void AttackPlayer()
    {
        if (PlayerInSight())
        {
            //Attack only when player is sight?
            if (_cooldownTimer >= _attackCoolDown)
            {
                _cooldownTimer = 0;
                _anim.SetTrigger("meleeAttack");
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z), 0, Vector2.left, 0, _playerLayer);

        if (hit.collider != null) _playerHealth = hit.transform.GetComponent<PlayerHealthSystem>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance, new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));

        Gizmos.DrawRay(_groundDetection.position, Vector2.down * _distance);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            _playerHealth.Damage(_damage);
        }
    }

}






