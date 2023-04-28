using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int ResourceGoal, ResourcesCollected;

    public void AddResources(int additional)
    {
        ResourcesCollected += additional;
    }

    public bool HasMetResourceGoal()
    {
        return ResourcesCollected >= ResourceGoal;
    }
}
