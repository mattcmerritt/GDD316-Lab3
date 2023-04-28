using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFollowWorkerState : AgentState
{
    private Worker Worker;
    private float MinFollowDistance = 3f;

    public GuardFollowWorkerState(Worker worker)
    {
        Worker = worker;
    }

    public override void ActivateState(Agent agent)
    {
        Debug.Log("GUARD: Started following " + Worker.gameObject.name);
        Worker.StartGuarding();
    }

    public override void Update(Agent agent)
    {
        // If not close to worker, walk to their position
        if (Vector3.Magnitude(Worker.transform.position - agent.transform.position) >= MinFollowDistance)
        {
            // Debug.Log("GUARD: Chasing");
            agent.GetNavAgent().SetDestination(Worker.transform.position);
            agent.GetNavAgent().isStopped = false;
        }
        else
        {
            // Debug.Log("GUARD: In Position");
            agent.GetNavAgent().isStopped = true;
        }

        // Stop following workers and defend the houses during the night
        DayManager dayManager = GameObject.FindObjectOfType<DayManager>();
        if (!dayManager.CheckIsDay())
        {
            agent.ChangeState(new GuardPatrolState());
        }
    }

    public override void OnTriggerEnter(Agent agent, Collider other)
    {
        // Do nothing, keep following worker
    }

    public override void EndState(Agent agent)
    {
        agent.GetNavAgent().isStopped = false;
        Worker.StopGuarding();
    }
}
