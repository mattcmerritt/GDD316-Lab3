using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightGuardState : AgentState
{
    private Guard Guard;
    private float MinAttackDistance = 2f;
    private bool InRangeForAttack = false;

    private float SwingDelay = 0.25f; // 0:15
    private float RecoveryDelay = 0.75f; // 0:45
    private bool Swinging = false;

    public EnemyFightGuardState(Guard guard)
    {
        Guard = guard;
    }

    public override void ActivateState(Agent agent)
    {
        Debug.Log("ENEMY: Attacking " + Guard.name);
        // show sword
        Enemy enemy = ((Enemy)agent);
        Animator ani = enemy.GetAnimator();
        ani.SetTrigger("Draw");
    }

    public override void Update(Agent agent)
    {
        // If not close to guard, close the distance
        if (Vector3.Magnitude(Guard.transform.position - agent.transform.position) >= MinAttackDistance)
        {
            // Debug.Log("ENEMY: Engaging");
            agent.GetNavAgent().SetDestination(Guard.transform.position);
            agent.GetNavAgent().isStopped = false;
            InRangeForAttack = false;
        }
        else
        {
            // Debug.Log("ENEMY: In Attack Range");
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
        Enemy enemy = ((Enemy)agent);
        if (enemy.GetIsAlive())
        {
            agent.GetNavAgent().isStopped = false;
            // put sword away
            Animator ani = enemy.GetAnimator();
            ani.SetTrigger("Hide");
        }
    }

    public IEnumerator SwingSword(Agent agent)
    {
        if (!Swinging)
        {
            Swinging = true;
            Enemy enemy = ((Enemy)agent);
            Animator ani = enemy.GetAnimator();
            // show sword
            ani.SetTrigger("Swing");
            yield return new WaitForSeconds(SwingDelay);

            // if the animation finishes and combat is still going, deal damage
            if (InRangeForAttack)
            {
                Guard.TakeDamage();

                // if guard gets taken out, roam again
                if (!Guard.GetIsConscious())
                {
                    agent.ChangeState(new EnemyRoamState());
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
        }
    }
}
