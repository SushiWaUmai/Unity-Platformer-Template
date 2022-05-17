using UnityEngine;
using DYP;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BasicMovementController2D _playerController;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private string _speedParameter = "speed";
    [SerializeField] private string _jumpParameter = "jump";

    private void OnValidate()
    {
        if (!_animator)
            _animator = GetComponent<Animator>();

        if (!_playerController)
            _playerController = GetComponent<BasicMovementController2D>();

        if (!_spriteRenderer)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _playerController.OnJump += OnStartJump;
        _playerController.OnLanded += OnEndJump;
        _playerController.OnFacingFlip += UpdateFlip;
    }

    private void OnDisable()
    {
        _playerController.OnJump -= OnStartJump;
        _playerController.OnLanded -= OnEndJump;
        _playerController.OnFacingFlip -= UpdateFlip;
    }

    private void FixedUpdate()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (string.IsNullOrEmpty(_speedParameter))
            return;

        _animator.SetFloat(_speedParameter, Mathf.Abs(_playerController.InputVelocity.x));
    }

    public void OnStartJump()
    {
        if (string.IsNullOrEmpty(_jumpParameter))
            return;

        _animator.SetBool(_jumpParameter, true);
    }

    public void OnEndJump()
    {
        if (string.IsNullOrEmpty(_jumpParameter))
            return;

        _animator.SetBool(_jumpParameter, false);
    }

    public void UpdateFlip(int direction)
    {
        if (direction > 0)
            _spriteRenderer.flipX = false;
        else if (direction < 0)
            _spriteRenderer.flipX = true;
    }
}

