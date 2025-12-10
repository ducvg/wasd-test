using System;
using UnityEngine;

public sealed class BallDetector : MonoBehaviour
{
    [SerializeField] private float detectRadius = 2f;
    [SerializeField] private LayerMask ballLayer;
    public Ball InRangeBall {get; private set;}
    public new Transform transform {get; private set;}
    private Collider[] colliderHitBuffer = new Collider[1];

    void Awake()
    {
        transform = base.transform;
    }

    void Update()
    {
        if(Physics.OverlapSphereNonAlloc(transform.position, detectRadius, colliderHitBuffer, ballLayer) <= 0)
        {
            InRangeBall = null;
            return;
        }
        InRangeBall = BallColliderMap.GetBallByCollider(colliderHitBuffer[0]);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(base.transform.position, detectRadius);
    }
#endif
}