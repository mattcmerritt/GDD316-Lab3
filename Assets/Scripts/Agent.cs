using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Parent class for all nav agents in the scene
// Contains the basic information for the agent to interact with the nav mesh
public class Agent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavAgent;
    protected AgentState ActiveState;

    // Helper method to retrieve the NavAgent component
    public NavMeshAgent GetNavAgent()
    {
        return NavAgent;
    }

    // Method to swap from one state to another
    public void ChangeState(AgentState state)
    {
        // End all coroutines currently running on the agent
        StopAllCoroutines();

        // Update state
        state.ActivateState(this);
        ActiveState = state;
    }
}