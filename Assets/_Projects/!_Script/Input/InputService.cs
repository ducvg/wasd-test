using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class InputService : PlayerInputAction.IPlayerActions
{
    public static Vector3 MoveInput {get; private set;}

    #region  privates
    private static InputService instance; 
    private static PlayerInputAction inputSystem;
    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void BootStrapService()
    {
        if(instance != null) return;
        instance = new(); 
    }

    InputService()
    {
        inputSystem ??= new PlayerInputAction();
        
        inputSystem.Player.SetCallbacks(this);
        
        SetMovementInputActive(true);
    }

    public static void SetMovementInputActive(bool isActive)
    {
        inputSystem.Player.Move.SetActive(isActive);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var input = inputSystem.Player.Move.ReadValue<Vector2>();
        MoveInput =  new Vector3(input.x, 0, input.y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    ~ InputService()
    {
        inputSystem.Dispose(); 
    }
}

public static class InputSystemExtension
{
    public static void SetActive(this InputAction inputAction, bool isActive)
    {
        if(isActive) inputAction.Enable();
        else inputAction.Disable();
    }
}