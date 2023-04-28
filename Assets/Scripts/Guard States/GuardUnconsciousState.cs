using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardUnconsciousState : AgentState
{
    private float HealDelay = 3f;

    public override void ActivateState(Agent agent)
    {
        // no movement while unconscious
        Debug.Log("GUARD: Lost consciousness!");
        agent.GetNavAgent().isStopped = true;
        agent.StartCoroutine(RegenerateHealth(agent));
    }

    public override void Update(Agent agent)
    {
        // Regain consciousness when health is back, and begin patrolling again
        Guard guard = ((Guard)agent);
        if (guard.GetIsConscious())
        {
            agent.ChangeState(new GuardPatrolState());
        }
    }

    public override void OnTriggerEnter(Agent agent, Collider other)
    {
        // Do nothing
    }

    public override void EndState(Agent agent)
    {
        agent.GetNavAgent().isStopped = false;
    }

    public IEnumerator RegenerateHealth(Agent agent)
    {
        Guard guard = ((Guard)agent);

        yield return new WaitForSeconds(HealDelay);

        // Continue healing
        guard.HealDamage();
        agent.StartCoroutine(RegenerateHealth(agent));
    }
}
