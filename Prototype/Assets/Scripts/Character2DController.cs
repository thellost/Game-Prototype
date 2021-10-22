using UnityEngine;
using UnityEngine.Events;

public class Character2DController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [Range(0, 1)] [SerializeField] private float _crouchSpeed = .36f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private bool _airControl = false;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _ceilingCheck;
    [SerializeField] private Collider2D _crouchDisableCollider;

    const float _groundedRadius = .2f;
    private bool _grounded;
    const float _ceilingRadius = .2f;
    private Rigidbody2D _rigidBody2D;
    private bool _facingRight = true;
    private Vector3 _velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool _wasCrouching = false;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }

        if (OnCrouchEvent == null)
        {
            OnCrouchEvent = new BoolEvent();
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _grounded;
        _grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(_ceilingCheck.position, _ceilingRadius, _whatIsGround))
            {
                crouch = true;
            }
        }

        if (_grounded || _airControl)
        {
            if (crouch)
            {
                if (!_wasCrouching)
                {
                    _wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= _crouchSpeed;

                if (_crouchDisableCollider != null)
                    _crouchDisableCollider.enabled = false;
            }
            else
            {
                if (_crouchDisableCollider != null)
                    _crouchDisableCollider.enabled = true;

                if (_wasCrouching)
                {
                    _wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 10f, _rigidBody2D.velocity.y);
            _rigidBody2D.velocity = Vector3.SmoothDamp(_rigidBody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

            if (move > 0 && !_facingRight)
            {
                Flip();
            }
            else if (move < 0 && _facingRight)
            {
                Flip();
            }
        }

        if (_grounded && jump)
        {
            _grounded = false;
            _rigidBody2D.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
