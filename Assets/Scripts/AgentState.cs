using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentState
{
    public abstract void ActivateState(Agent agent);
    public abstract void Update(Agent agent);
}
