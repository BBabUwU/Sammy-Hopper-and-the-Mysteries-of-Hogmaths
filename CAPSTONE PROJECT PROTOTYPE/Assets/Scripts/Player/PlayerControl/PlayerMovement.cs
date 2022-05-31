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
  private void Awake()
  {
    _rb = gameObject.GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
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
    if (IsGrounded())
    {
      _coyoteTimeCounter = _coyoteTime;
    }
    else
    {
      _coyoteTimeCounter -= Time.deltaTime;
    }

    if (Input.GetButtonDown("Jump"))
    {
      _jumpBufferCounter = _jumpBufferTime;
    }
    else
    {
      _jumpBufferCounter -= Time.deltaTime;
    }

    if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
    {
      _rb.velocity = new Vector2(_rb.velocity.x, _jumpingPower);

      _jumpBufferCounter = 0f;
    }

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
}
