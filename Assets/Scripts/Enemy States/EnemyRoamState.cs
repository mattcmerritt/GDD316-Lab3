using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamState : AgentState
{
    private float PauseAtEndpoint = 1;
    private float MinDistanceFromDestination = 1f;
    private bool Waiting = false;
    private Vector3 Destination;

    public override void ActivateState(Agent agent)
    {
        Debug.Log("ENEMY: Started to roam");
        Enemy enemy = ((Enemy)agent);

        Destination = enemy.FindReachableLocation();
        agent.GetNavAgent().SetDestination(Destination);
    }

    public override void Update(Agent agent)
    {
        // Debug.Log("ENEMY: Roaming to " + Destination);
        if (!Waiting)
        {            
            // if close to the current destination, start waiting for the delay, then move on
            if (Vector3.Magnitude(Destination - agent.transform.position) < MinDistanceFromDestination)
            {
                agent.StartCoroutine(WaitAtDestination(agent));
            }
        }
    }

    public override void OnTriggerEnter(Agent agent, Collider other)
    {
        // attack any conscious guards
        if (other.gameObject.tag == "Guard")
        {
            Guard foundGuard = other.GetComponent<Guard>();
            if (foundGuard.GetIsConscious())
            {
                agent.ChangeState(new EnemyFightGuardState(foundGuard));
            }
        }
        // scare any workers
        if (other.gameObject.tag == "Worker")
        {
            Worker foundWorker = other.GetComponent<Worker>();
            if (!foundWorker.GetIsScared())
            {
                agent.ChangeState(new EnemyChaseWorkerState(foundWorker));
            }
        }
    }

    public override void EndState(Agent agent)
    {
        // Nothing additional to do
    }

    public IEnumerator WaitAtDestination(Agent agent)
    {
        Enemy enemy = ((Enemy)agent);

        Waiting = true;
        yield return new WaitForSeconds(PauseAtEndpoint);

        Destination = enemy.FindReachableLocation();
        agent.GetNavAgent().SetDestination(Destination);

        Waiting = false;
    }
}
