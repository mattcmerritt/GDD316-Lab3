using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Parent class for all nav agents in the scene
// Contains the basic information for the agent to interact with the nav mesh
public class Agent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavAgent;

    public NavMeshAgent GetNavAgent()
    {
        return NavAgent;
    }
}
