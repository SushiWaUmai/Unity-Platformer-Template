using DYP;
using UnityEngine;

public class PlayerGameInput : MonoBehaviour
{
    [SerializeField] private BasicMovementController2D _controller;
    private InputMaster _controls;

    public void OnEnable()
    {
        if (_controls == null)
            _controls = new InputMaster();

        _controls.Player.Enable();
    }

    public void OnDisable()
    {
        _controls.Player.Disable();
    }

    private void Update()
    {
        Vector2 axis = _controls.Player.Move.ReadValue<Vector2>();

        bool jump = _controls.Player.Jump.WasPressedThisFrame();

        bool holdingJump = _controls.Player.Jump.IsPressed();
        bool releaseJump = _controls.Player.Jump.WasReleasedThisFrame();

        _controller.InputMovement(axis);

        _controller.PressJump(jump);
        _controller.HoldJump(holdingJump);
        _controller.ReleaseJump(releaseJump);
    }
}