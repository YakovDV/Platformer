using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput), typeof(Health))]

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GroundSensor _groundSensor;

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private Rotator _horizontalTurn;
    private Jumper _jumper;
    private Health _health;
    private Vector2 _horizontalVelocity;

    public float HorizontalSpeedNormalized { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _horizontalTurn = GetComponent<Rotator>();
        _jumper = GetComponent<Jumper>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _playerInput.JumpPressed += OnJumpPressed;
    }

    private void OnDisable()
    {
        _playerInput.JumpPressed -= OnJumpPressed;
    }

    private void FixedUpdate()
    {
        if (_health.IsDead == true)
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
            return;
        }

        MoveHorizontal();
        _horizontalTurn.TurnToMovement(_horizontalVelocity.x);
    }

    private void MoveHorizontal()
    {
        _horizontalVelocity = _playerInput.InputDirection * _speed;
        _rigidbody.velocity = new(_horizontalVelocity.x, _rigidbody.velocity.y);

        HorizontalSpeedNormalized = _horizontalVelocity.magnitude / _speed;
    }

    private void OnJumpPressed()
    {
        if (_groundSensor.IsGrounded() == false || _health.IsDead == true)
        {
            return;
        }

        _jumper.Jump(_rigidbody);
    }
}