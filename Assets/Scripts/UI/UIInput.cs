using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour, InputMaster.IMenuActions
{
    private InputMaster _controls;

    public void OnEnable()
    {
        if (_controls == null)
            _controls = new InputMaster();

        _controls.Menu.SetCallbacks(this);
        _controls.Menu.Enable();
    }

    public void OnDisable()
    {
        _controls.Menu.Disable();
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            PauseMenuManager.Instance.TogglePause();
    }
}
