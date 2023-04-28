using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrolState : AgentState
{
    private float PauseAtEndpoint = 2;
    private bool CurrentlyAtStart = false;
    private float MinDistanceFromEndpoint = 0.25f;
    private bool Waiting = false;

    public override void ActivateState(Agent agent)
    {
        Debug.Log("GUARD: Started to patrol");
        Guard guard = ((Guard)agent);
        agent.GetNavAgent().SetDestination(guard.GetPatrolStartEndpoint());
    }

    public override void Update(Agent agent)
    {
        if (!Waiting)
        {
            Guard guard = ((Guard)agent);
            Vector3 endpoint = CurrentlyAtStart ? guard.GetPatrolEndEndpoint() : guard.GetPatrolStartEndpoint();

            // if close to the current endpoint, start waiting for the delay, then move on
            if (Vector3.Magnitude(endpoint - agent.transform.position) < MinDistanceFromEndpoint)
            {
                agent.StartCoroutine(WaitAtEndpoint(agent));
            }
        }
    }

    public override void OnTriggerEnter(Agent agent, Collider other)
    {
        // not implemented
    }

    public IEnumerator WaitAtEndpoint(Agent agent)
    {
        Guard guard = ((Guard)agent);

        Waiting = true;
        yield return new WaitForSeconds(PauseAtEndpoint);

        CurrentlyAtStart = !CurrentlyAtStart;
        Vector3 newEndpoint = CurrentlyAtStart ? guard.GetPatrolEndEndpoint() : guard.GetPatrolStartEndpoint();
        agent.GetNavAgent().SetDestination(newEndpoint);

        Waiting = false;
    }
}
