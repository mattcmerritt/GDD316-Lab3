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

    // Delegate this task to the current state
    private void Update()
    {
        if (ActiveState != null)
        {
            ActiveState.Update(this);
        }
    }

    // Delegate this task to the current state
    private void OnTriggerEnter(Collider other)
    {
        if (ActiveState != null)
        {
            ActiveState.OnTriggerEnter(this, other);
        }
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
