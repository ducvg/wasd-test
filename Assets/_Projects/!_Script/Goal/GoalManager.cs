using UnityEngine;

public sealed class GoalManager : Singleton<GoalManager>
{
    [field: SerializeField] public Goal LeftGoal {get; private set;}
    [field: SerializeField] public Goal RightGoal {get; private set;}

    public ObjectDistance<Goal> GetNearestGoal(Vector3 origin)
    {
        if(!LeftGoal || !RightGoal) return default;
        
        ObjectDistance<Goal> goalDist = new();
        float leftDist = Vector3.Distance(origin, LeftGoal.transform.position);
        float rightDist = Vector3.Distance(origin, RightGoal.transform.position);
        if(leftDist < rightDist)
        {
            goalDist.obj = LeftGoal;
            goalDist.dist = leftDist;
        } else {
            goalDist.obj = RightGoal;
            goalDist.dist = rightDist;    
        }
        return goalDist;
    }
}