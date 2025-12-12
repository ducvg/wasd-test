using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform playerCamTf;
    [SerializeField] private Rigidbody rb;
    [field: SerializeField] public float MaxMoveSpeed {get; private set;} = 4f;
    [SerializeField] private float moveSpeedRampUpFactor = 0.4f;
    [SerializeField] private float moveSpeedRampDownFactor = 0.2f;
    [SerializeField] private float turnSpeed = 3f;
    public new Transform transform {get; private set;}
    public Vector3 MoveVelocity {get; private set;}

    void Awake()
    {
        transform = base.transform;
    }

    void FixedUpdate()
    {
        Vector3 moveDir = Quaternion.AngleAxis(playerCamTf.eulerAngles.y, Vector3.up) * InputService.MoveInput;

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
        rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);
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
        rb.MovePosition(rb.position + MoveVelocity * Time.fixedDeltaTime);
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
