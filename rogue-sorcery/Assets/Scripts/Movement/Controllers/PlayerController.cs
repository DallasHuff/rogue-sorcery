using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    private PlayerInputActions _inputActions;
    private bool _isJumping;
    private bool _isDashing;
    private bool _isCast2;

    private void OnEnable()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Jump.started += JumpStarted;
        _inputActions.Player.Jump.canceled += JumpCanceled;
        _inputActions.Player.Dash.started += DashStarted;
        _inputActions.Player.Dash.canceled += DashCanceled;
        _inputActions.Player.Cast2.started += Cast2Started;
        _inputActions.Player.Cast2.canceled += Cast2Canceled;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
        _inputActions.Player.Jump.started -= JumpStarted;
        _inputActions.Player.Jump.canceled -= JumpCanceled;
        _inputActions.Player.Dash.started -= DashStarted;
        _inputActions.Player.Dash.canceled -= DashCanceled;
        _inputActions.Player.Cast2.started -= Cast2Started;
        _inputActions.Player.Cast2.canceled -= Cast2Canceled;
        _inputActions = null;
    }

    private void JumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isJumping = false;
    }
    private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isJumping = true;
    }

    private void DashCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isDashing = false;
    }
    private void DashStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isDashing = true;
    }

    private void Cast2Started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isCast2 = true;
    }
    private void Cast2Canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _isCast2 = false;
    }

    public override float RetrieveMoveInput()
    {
        return _inputActions.Player.Move.ReadValue<Vector2>().x;
    }

    public override bool RetrieveJumpInput()
    {
        return _isJumping;
    }

    public override bool RetrieveDashInput()
    {
        return _isDashing;
    }
    public override bool RetrieveCast2Input()
    {
        return _isCast2;
    }
}
