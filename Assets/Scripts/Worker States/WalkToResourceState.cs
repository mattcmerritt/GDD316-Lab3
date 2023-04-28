using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToResourceState : AgentState
{
    private ResourceSource Resource;

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

    public override void OnTriggerEnter(Agent agent, Collider other)
    {
        // if agent has reached its destination, start gathering
        if (other.gameObject == Resource.gameObject)
        {
            agent.ChangeState(new GatherResourceState(Resource));
        }
    }
}