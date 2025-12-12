using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class Goal : MonoBehaviour
{
    [SerializeField] private ParticleSystem goalParticle;
    private Collision lastBallCollision;

    async void OnCollisionEnter(Collision collision)
    {
        if(!BallColliderMap.GetBallByCollider(collision.collider)) return; //not ball
        if(collision == lastBallCollision) return;

        goalParticle.Play();
        await UniTask.Delay(2000, cancellationToken: destroyCancellationToken).SuppressCancellationThrow();
        lastBallCollision = null;
        CameraManager.Instance.FollowPlayer();
    }
}