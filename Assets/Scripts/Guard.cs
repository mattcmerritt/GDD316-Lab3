using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Agent
{
    [SerializeField] private Vector3 PatrolStartPosition, PatrolEndPosition;
    [SerializeField] private int MaximumHealth = 5, Health = 5;
    [SerializeField] private bool IsConscious = true;

    [SerializeField] private Animator Animator;

    // Start by patrolling
    private void Start()
    {
        ChangeState(new GuardPatrolState());
    }

    // Helper method to get patrol locations
    public Vector3 GetPatrolStartEndpoint()
    {
        return PatrolStartPosition;
    }

    // Helper method to get patrol locations
    public Vector3 GetPatrolEndEndpoint()
    {
        return PatrolEndPosition;
    }

    // Helper method to deal damage to the guard
    public void TakeDamage()
    {
        Debug.Log(gameObject.name + " took damage!");

        Health--;

        // take away consciousness if health is depleted
        if (Health <= 0)
        {
            IsConscious = false;
            ChangeState(new GuardUnconsciousState());
        }
    }

    // Helper method to restore health to the guard when beaten
    public void HealDamage()
    {
        if (Health < MaximumHealth)
        {
            Health++;
        }

        // restore consciousness when health is full again
        if (!IsConscious && Health == MaximumHealth)
        {
            IsConscious = true;
        }
    }

    // Helper method to check guard's consciousness
    public bool GetIsConscious()
    {
        return IsConscious;
    }

    // Helper method to access weapon swing animations
    public Animator GetAnimator()
    {
        return Animator;
    }
}
