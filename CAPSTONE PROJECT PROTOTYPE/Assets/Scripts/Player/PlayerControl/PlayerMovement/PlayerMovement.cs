using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontal;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpingPower = 16f;
    private bool _isFacingRight = true;
    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;

    private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    private Rigidbody2D _rb;
    public Animator animator;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    //Events
    private void OnEnable()
    {
        PlayerHealthSystem._OnPlayerDeath += DisablePlayerMovement;
    }

    private void OnDisable()
    {
        PlayerHealthSystem._OnPlayerDeath -= DisablePlayerMovement;
    }


    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        EnablePlayerMovement();
    }

    private void Update()
    {
        animator.SetFloat("Falling", _rb.velocity.y);
        Jump();
        Flip();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(_horizontal));
        _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
    }

    private void Jump()
    {

        //animation
        if (IsGrounded()) animator.SetBool("IsJumping", false);
        else if (!IsGrounded()) animator.SetBool("IsJumping", true);
        //Monitoring coyote timer
        if (IsGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        //Monitoring jump buffer timer
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        //Short jump
        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpingPower);

            _jumpBufferCounter = 0f;
        }

        //High jump
        if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
            _coyoteTimeCounter = 0f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void DisablePlayerMovement()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }

    private void EnablePlayerMovement()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
