using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class BallKicker : MonoBehaviour
{
    [SerializeField] private BallDetector ballDetector;
    [SerializeField] private Vector2 reachGoalTimeRange = new(0.5f, 1);

    void Update()
    {
        CheckKickable(ballDetector.InRangeBall);
    }

    void CheckKickable(bool isActive)
    {
        UIManager.Instance.SetKickBtnActive(isActive);
        if(!isActive) return;
        UIManager.Instance.SetKickBtnOnBall(ballDetector.InRangeBall);
    }

    public void NormalKick()
    {
        if(!ballDetector.InRangeBall) return;

        ObjectDistance<Goal> goalDist = GoalManager.Instance.GetNearestGoal(transform.position);
        if(!goalDist.obj) return;

        KickToGoal(ballDetector.InRangeBall, goalDist.obj);
    }

    public void AutoKick()
    {
        ObjectDistance<Ball> ballDist = BallManager.Instance.GetFurthestBall(transform.position);
        if(!ballDist.obj) return;
        ObjectDistance<Goal> goalDist = GoalManager.Instance.GetNearestGoal(ballDist.obj.transform.position);
        if(!goalDist.obj) return;

        KickToGoal(ballDist.obj, goalDist.obj, delayMs: 500);
    }

    public async void KickToGoal(Ball kickedBall, Goal targetGoal, int delayMs = 0)
    {
        CameraManager.Instance.FollowBall(kickedBall);
        await UniTask.Delay(delayMs, cancellationToken: destroyCancellationToken).SuppressCancellationThrow();
        
        Vector3 delta = targetGoal.transform.position - kickedBall.transform.position;
        float reachTime = GetReachTimeByDistance(delta.magnitude);
        Vector3 initialVelocity = (delta - 0.5f * Physics.gravity * (reachTime * reachTime)) / reachTime;

        kickedBall.SetVelocity(initialVelocity);
    }

    private float GetReachTimeByDistance(float distance)
    {
        var distRange = new Vector2(2f, 10f);
        var scaledDist = Mathf.InverseLerp(distRange.x, distRange.y, distance);
        return Mathf.Lerp(reachGoalTimeRange.x, reachGoalTimeRange.y, scaledDist);
    }
}

public struct ObjectDistance<T>
{
    public T obj;
    public float dist;
}
