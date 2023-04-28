using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Agent
{
    private AgentState ActiveState;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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

            WalkToResourceState walkToTree = new WalkToResourceState(closest);

            walkToTree.ActivateState(this);
            ActiveState = walkToTree;
        }

        if (ActiveState != null)
        {
            ActiveState.Update(this);
        }
    }
}
