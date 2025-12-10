using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class AnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private Animator animator;
    int blendHash = Animator.StringToHash("Blend");

    void Update()
    {
        UpdateMovementAnimation();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void UpdateMovementAnimation()
    {
        float blendMovement = Mathf.InverseLerp(0, playerMover.MaxMoveSpeed, playerMover.MoveVelocity.magnitude); 
        animator.SetFloat(blendHash, blendMovement);
    }
}