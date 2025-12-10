using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform playerCamTf;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float turnSpeed = 3f;
    [field: SerializeField] public float MaxMoveSpeed {get; private set;} = 4f;
    [SerializeField] private float moveSpeedRampUpFactor = 0.4f;
    [SerializeField] private float moveSpeedRampDownFactor = 0.2f;
    public new Transform transform {get; private set;}
    public Vector3 MoveVelocity {get; private set;}
    private Vector3 moveInput;

    void Awake()
    {
        transform = base.transform;
    }

    void Update()
    {
        moveInput = InputController.GetMovementInput();
    }

    void FixedUpdate()
    {
        Vector3 moveDir = Quaternion.AngleAxis(playerCamTf.eulerAngles.y, Vector3.up) * moveInput;

        if (moveDir.sqrMagnitude > 0.001f)
        {
            Move(moveDir);
            Rotate(moveDir);
        }
        else
        {
            StopMove();
        }
    }

    void Move(Vector3 moveDir)
    {
        Vector3 targetVelocity = moveDir * MaxMoveSpeed;
        MoveVelocity = Vector3.MoveTowards(MoveVelocity, targetVelocity, moveSpeedRampUpFactor);
        rb.velocity = MoveVelocity.WithY(rb.velocity.y);
    }

    void Rotate(Vector3 lookDir) 
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookDir);
        Quaternion step = Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed);
        rb.MoveRotation(step);
    }

    void StopMove()
    {
        MoveVelocity = Vector3.MoveTowards(MoveVelocity, Vector3.zero, moveSpeedRampDownFactor);
        rb.velocity = MoveVelocity.WithY(rb.velocity.y);
    }
}

public static class VectorExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithY(this Vector3 vector3, float y)
    {
        return new Vector3(vector3.x, y, vector3.z);
    }
}
