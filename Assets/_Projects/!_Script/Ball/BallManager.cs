using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class BallManager : Singleton<BallManager>
{
    [SerializeField] private List<Ball> ballList;

    public ObjectDistance<Ball> GetNearestBall(Vector3 origin)
    {
        if(ballList.Count <= 0) return default;
        ObjectDistance<Ball> ballDist = new()
        {
            obj = null,
            dist = float.MaxValue
        };
        foreach(var ball in ballList)
        {
            float dist = Vector3.Distance(ball.transform.position, origin);
            if(dist < ballDist.dist)
            {
                ballDist.obj = ball;
                ballDist.dist = dist;
            }
        }
        return ballDist;
    }

    public ObjectDistance<Ball> GetFurthestBall(Vector3 origin)
    {
        if(ballList.Count <= 0) return default;
        
        ObjectDistance<Ball> ballDist = new()
        {
            obj = null,
            dist = -1
        };
        foreach(var ball in ballList)
        {
            float dist = Vector3.Distance(ball.transform.position, origin);
            if(dist > ballDist.dist)
            {
                ballDist.dist = dist;
                ballDist.obj = ball;
            }
        }
        return ballDist;
    }


#if UNITY_EDITOR
    [ContextMenu("Fetch Balls On Scene")]
    void FetchBalls()
    {
        ballList = FindObjectsByType<Ball>(FindObjectsSortMode.None).ToList();
    }
#endif
}