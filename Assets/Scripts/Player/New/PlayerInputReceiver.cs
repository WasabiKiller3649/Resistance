using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReceiver : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput = default;
    [SerializeField] private InputActionReference _attackInput = default;

    private InputAction _moveAction = default;
    private InputAction _attackAction = default;

    public Vector2 MoveValue => _moveAction.ReadValue<Vector2>();
    public bool IsAttackPress => _attackAction.IsPressed();

    public PlayerInputReceiver Initialize()
    {
        if (_moveInput != null)
        {
            _moveAction = _moveInput.action;
        }

        if(_attackInput != null)
        {
            _attackAction = _attackInput.action;
        }

        EnableAction();

        return this;
    }

    private void EnableAction()
    {
        _moveAction?.Enable();
        _attackAction?.Enable();
    }

    private void DisableAction()
    {
        _moveAction.Disable();
        _attackAction.Disable();
    }
}