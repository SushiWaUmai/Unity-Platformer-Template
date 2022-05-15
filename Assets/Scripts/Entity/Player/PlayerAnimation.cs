using UnityEngine;
using DYP;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BasicMovementController2D _playerController;

    [SerializeField] private string _speedParameter = "speed";
    [SerializeField] private string _jumpTrigger = "jump";

    private void OnValidate()
    {
        if (!_animator)
            _animator = GetComponent<Animator>();

        if (!_playerController)
            _playerController = GetComponent<BasicMovementController2D>();
    }

    private void OnEnable()
    {
        _playerController.OnJump += OnJump;
    }

    private void OnDisable()
    {
        _playerController.OnJump -= OnJump;
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

    public void OnJump()
    {
        if (string.IsNullOrEmpty(_jumpTrigger))
            return;

        _animator.SetTrigger(_jumpTrigger);
    }
}

