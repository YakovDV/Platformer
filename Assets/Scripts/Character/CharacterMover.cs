using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput), typeof(Character))]

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 8f;

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private Character _character;
    private HorizontalTurn2D _horizontalTurn;
    private Vector2 _horizontalVelocity;

    public float NormalizedHorizontalSpeed { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _character = GetComponent<Character>();
        _horizontalTurn = GetComponent<HorizontalTurn2D>();
    }

    private void FixedUpdate()
    {
        if (_character.IsDead == true)
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
            return;
        }

        MoveHorizontal();
        _horizontalTurn.TurnToMovement(_horizontalVelocity.x);

        bool isJumpRequested = _playerInput.IsJumpRequested;
        _playerInput.ResetJumpRequest();

        if (isJumpRequested == true && _character.IsGrounded == true)
        {
            Jump();
        }
    }

    private void MoveHorizontal()
    {
        _horizontalVelocity = _playerInput.InputDirection * _speed;
        _rigidbody.velocity = new(_horizontalVelocity.x, _rigidbody.velocity.y);

        NormalizedHorizontalSpeed = _horizontalVelocity.magnitude / _speed;
    }

    private void Jump()
    {
        Vector2 jumpDirection = new(0, _jumpForce);

        _rigidbody.AddForce(jumpDirection, ForceMode2D.Impulse);

        _playerInput.ResetJumpRequest();
    }
}