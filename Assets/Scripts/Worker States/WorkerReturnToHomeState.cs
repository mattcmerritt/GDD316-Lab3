using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerReturnToHomeState : AgentState
{
    public override void ActivateState(Agent agent)
    {
        Debug.Log("Returning home");
        agent.GetNavAgent().SetDestination(((Worker)agent).GetHomePosition());
    }

    public override void Update(Agent agent)
    {
        // Does nothing
    }

    public override void OnTriggerEnter(Agent agent, Collider other)
    {
        // Do nothing, just keep running to home
    }
}
