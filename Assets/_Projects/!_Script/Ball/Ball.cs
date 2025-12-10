using UnityEngine;

public sealed class Ball : MonoBehaviour
{
    [SerializeField] Collider ballCollider;
    [SerializeField] Rigidbody rb;
    public new Transform transform {get; private set;}

    void Awake()
    {
        transform = base.transform;
        BallColliderMap.AddBallCollider(ballCollider, this);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    void OnDestroy()
    {
        BallColliderMap.RemoveBallCollider(ballCollider);        
    }
    
}