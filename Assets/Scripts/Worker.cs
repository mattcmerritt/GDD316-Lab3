using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Agent
{
    [SerializeField] private int HeldResources = 0, ResourceCapacity = 5;
    [SerializeField] private Vector3 HomePosition = Vector3.zero;

    // Start by picking the closest tree, and gathering from there
    private void Start()
    {
        // Find the closest tree
        ResourceSource[] trees = FindObjectsOfType<ResourceSource>();
        float distanceToClosest = Mathf.Infinity;
        ResourceSource closest = null;
        foreach (ResourceSource tree in trees)
        {
            float distanceToTree = Vector3.Magnitude(tree.transform.position - transform.position);
            if (distanceToTree <= distanceToClosest)
            {
                closest = tree;
                distanceToClosest = distanceToTree;
            }
        }

        // Create the new state and assign it to this worker
        WalkToResourceState walkToTree = new WalkToResourceState(closest);
        ChangeState(walkToTree);
    }

    // Delegate this task to the current state
    private void Update()
    {
        if (ActiveState != null)
        {
            ActiveState.Update(this);
        }
    }

    // Delegate this task to the current state
    private void OnTriggerEnter(Collider other)
    {
        if (ActiveState != null)
        {
            ActiveState.OnTriggerEnter(this, other);
        }
    }

    // Helper method to check if the worker can pick up more
    public bool CanHoldMoreResources()
    {
        return HeldResources < ResourceCapacity;
    }

    // Helper method to pick a single resource
    public void PickUpResource()
    {
        HeldResources += 1;
    }

    // Helper method to remove resources from the worker's hands
    public void DropResources()
    {
        HeldResources = 0;
    }

    // Helper method to retrieve home position
    public Vector3 GetHomePosition()
    {
        return HomePosition;
    }
}
