using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy settings")]
    [SerializeField] private float _enemySpeed = 2f;
    private bool _enemyIsMoving;
    private bool _enemyIsMovingRight;
    [SerializeField] private float _enemyDamage = 10f;
    //Rigidbodies
    Rigidbody2D _enemyRb;
    [Header("Detection objects")]
    public GameObject _groundDetection;
    public GameObject _obstacleDetection;
    PlayerHealthSystem _playerHealth;
    [Header("Detection settings")]
    [SerializeField] private float _groundDistance = 5f;
    [SerializeField] private float _obstacleDistance = 5f;
    //Raycast
    RaycastHit2D _hitGround;
    RaycastHit2D _hitObstacle;
    //Animations
    private Animator _enemyAnim;

    private void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyAnim = GetComponent<Animator>();
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
        _enemyIsMovingRight = true;
        _enemyIsMoving = true;
    }


    private void FixedUpdate()
    {
        _enemyAnim.SetBool("isMoving", _enemyIsMoving);
        EnemyPatrol();
    }

    private void EnemyPatrol()
    {
        _enemyIsMoving = true;

        if (GroundDetected())
        {
            if (_enemyIsMovingRight)
            {
                _enemyRb.velocity = new Vector2(_enemySpeed, 0);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (!_enemyIsMovingRight)
            {
                _enemyRb.velocity = new Vector2(-_enemySpeed, 0);
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }

        if (!GroundDetected() || ObstacleDetected())
        {
            if (_enemyIsMovingRight)
            {
                _enemyIsMovingRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                _enemyIsMovingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

    }

    //Raycast for ground and obstacle detection
    private bool GroundDetected()
    {
        _hitGround = Physics2D.Raycast(_groundDetection.transform.position, Vector2.down, _groundDistance, LayerMask.GetMask("Ground"));
        return _hitGround.collider;
    }

    private bool ObstacleDetected()
    {
        _hitObstacle = Physics2D.Raycast(_obstacleDetection.transform.position, Vector2.right, _obstacleDistance, LayerMask.GetMask("Ground") | LayerMask.GetMask("Wall"));
        return _hitObstacle.collider;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") _playerHealth.Damage(_enemyDamage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Ground distance ray
        Gizmos.DrawRay(_groundDetection.transform.position, Vector2.down * _groundDistance);
        //Obstacle distance ray
        Gizmos.DrawRay(_obstacleDetection.transform.position, Vector2.right * _obstacleDistance);
    }

}
