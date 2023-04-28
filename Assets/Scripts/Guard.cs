using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Agent
{
    [SerializeField] private Vector3 PatrolStartPosition, PatrolEndPosition;

    // Start by patrolling
    private void Start()
    {
        ChangeState(new GuardPatrolState());
    }

    // Helper method to get patrol locations
    public Vector3 GetPatrolStartEndpoint()
    {
        return PatrolStartPosition;
    }

    // Helper method to get patrol locations
    public Vector3 GetPatrolEndEndpoint()
    {
        return PatrolEndPosition;
    }
}
