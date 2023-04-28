using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToResourceState : AgentState
{
    [SerializeField] private ResourceSource Resource;

    public WalkToResourceState(ResourceSource resource)
    {
        Resource = resource;
    }

    public override void ActivateState(Agent agent)
    {
        Debug.Log("Started to walk towards " + Resource.gameObject.name);
        agent.GetNavAgent().SetDestination(Resource.transform.position);
    }

    public override void Update(Agent agent)
    {
        // agent.GetNavAgent().SetDestination(Resource.transform.position);
    }
}
