using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class BallColliderMap
{
    private static Dictionary<Collider, Ball> ballColliderDict = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddBallCollider(Collider collider, Ball ball) => ballColliderDict[collider] = ball;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveBallCollider(Collider collider) => ballColliderDict.Remove(collider);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ball GetBallByCollider(Collider collider) 
    {
        if(ballColliderDict.TryGetValue(collider, out Ball ball))
        {
            return ball;
        }
        return null;
    }
}