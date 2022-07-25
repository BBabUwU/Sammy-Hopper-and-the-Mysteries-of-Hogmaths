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

    private bool _isAbleToMove;

    //Events
    private void OnEnable()
    {
        PlayerManager._disableControls += DisablePlayerMovement;
        PlayerManager._enableControls += EnablePlayerMovement;
    }

    private void OnDisable()
    {
        PlayerManager._disableControls -= DisablePlayerMovement;
        PlayerManager._enableControls -= EnablePlayerMovement;
    }

    private void DisablePlayerMovement()
    {
        _isAbleToMove = false;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
    private void EnablePlayerMovement()
    {
        _isAbleToMove = true;
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
        if (_isAbleToMove)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(_horizontal));
            _rb.velocity = new Vector2(_horizontal * _speed, _rb.velocity.y);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    private void Jump()
    {
        if (_isAbleToMove)
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
}
