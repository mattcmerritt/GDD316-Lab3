using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Agent
{
    [SerializeField] private float MapBounds;
    [SerializeField] private Animator Animator;
    [SerializeField] private int MaximumHealth = 5, Health = 5;
    [SerializeField] private bool IsAlive = true;
    [SerializeField] private Vector3 StartLocation;

    // Start by roaming
    private void Start()
    {
        StartLocation = transform.position;
        ChangeState(new EnemyRoamState());
    }

    public void Reset()
    {
        transform.position = StartLocation;
        ChangeState(new EnemyRoamState());
    }

    // Helper method to come up with a random point from the map to go to
    public Vector3 FindReachableLocation()
    {
        // Ensure that there is not a house or tree that would prevent the enemy from reaching it
        bool locationValid = false;
        Vector3 target = Vector3.zero;
        // Generate new locations until one is reachable
        while (!locationValid)
        {
            target = new Vector3(Random.Range(-MapBounds, MapBounds), 1f, Random.Range(-MapBounds, MapBounds));
            if (Physics.Raycast(target + Vector3.up * 5, Vector3.down, out RaycastHit hit))
            {
                if (hit.collider.gameObject.tag != "Resource" && hit.collider.gameObject.tag != "Obstacle")
                {
                    locationValid = true;
                }
            }
        }

        return target;
    }

    // Helper method to access weapon swing animations
    public Animator GetAnimator()
    {
        return Animator;
    }

    // Helper method to deal damage to the guard
    public void TakeDamage()
    {
        Debug.Log(gameObject.name + " took damage!");

        Health--;

        // remove if health is depleted
        if (Health <= 0)
        {
            IsAlive = false;
            gameObject.SetActive(false);
            ChangeState(null);
        }
    }

    // Helper method to check if alive
    public bool GetIsAlive()
    {
        return IsAlive;
    }
}
