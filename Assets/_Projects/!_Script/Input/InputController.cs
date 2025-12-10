using System.Runtime.CompilerServices;
using UnityEngine;

public static class InputController
{
    private static PlayerInputAction inputActions;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnLoad()
    {
        inputActions ??= new PlayerInputAction();
        SetMovementInputActive(true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 GetMovementInput()
    {
        var input = inputActions.Player.Move.ReadValue<Vector2>();
        return new Vector3(input.x, 0, input.y);
    }

    public static void SetMovementInputActive(bool isActive)
    {
        if(isActive) inputActions.Player.Move.Enable();
        else inputActions.Player.Move.Disable();
    }
}