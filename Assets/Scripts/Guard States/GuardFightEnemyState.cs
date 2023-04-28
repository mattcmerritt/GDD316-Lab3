using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFightEnemyState : AgentState
{
    private Enemy Enemy;
    private float MinAttackDistance = 2f;
    private bool InRangeForAttack = false;

    private float SwingDelay = 0.25f; // 0:15
    private float RecoveryDelay = 0.75f; // 0:45
    private bool Swinging = false;

    public GuardFightEnemyState(Enemy enemy)
    {
        Enemy = enemy;
    }

    public override void ActivateState(Agent agent)
    {
        Debug.Log("GUARD: Attacking " + Enemy.name);
        // show sword
        Guard guard = ((Guard)agent);
        Animator ani = guard.GetAnimator();
        ani.SetTrigger("Draw");
    }

    public override void Update(Agent agent)
    {
        // If not close to guard, close the distance
        if (Vector3.Magnitude(Enemy.transform.position - agent.transform.position) >= MinAttackDistance)
        {
            // Debug.Log("GUARD: Engaging");
            agent.GetNavAgent().SetDestination(Enemy.transform.position);
            agent.GetNavAgent().isStopped = false;
            InRangeForAttack = false;
        }
        else
        {
            // Debug.Log("GUARD: In Attack Range");
            agent.GetNavAgent().isStopped = true;
            InRangeForAttack = true;
            // Swing sword
            agent.StartCoroutine(SwingSword(agent));
        }
    }

    public override void OnTriggerEnter(Agent agent, Collider other)
    {
        // Not implemented yet
    }

    public override void EndState(Agent agent)
    {
        agent.GetNavAgent().isStopped = false;
        // put sword away
        Guard guard = ((Guard)agent);
        Animator ani = guard.GetAnimator();
        ani.SetTrigger("Hide");
    }

    public IEnumerator SwingSword(Agent agent)
    {
        if (!Swinging)
        {
            Swinging = true;
            Guard guard = ((Guard)agent);
            Animator ani = guard.GetAnimator();
            // show sword
            ani.SetTrigger("Swing");
            yield return new WaitForSeconds(SwingDelay);

            // if the animation finishes and combat is still going, deal damage
            if (InRangeForAttack)
            {
                Enemy.TakeDamage();

                // if enemy gets taken out, patrol again
                if (!Enemy.GetIsAlive())
                {
                    agent.ChangeState(new GuardPatrolState());
                }
                // otherwise, keep attacking
                else
                {
                    ani.SetTrigger("Reset");
                    yield return new WaitForSeconds(RecoveryDelay);
                    Swinging = false;
                    agent.StartCoroutine(SwingSword(agent));
                }
            }
            else
            {
                ani.SetTrigger("Reset");
                yield return new WaitForSeconds(RecoveryDelay);
                Swinging = false;
            }
        }
    }
}
