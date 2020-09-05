using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GuardBehavior : VersionedMonoBehaviour
{
    public Transform[] targets;
    Transform currentTarget;
    /// <summary>Time in seconds to wait at each target</summary>
    public float delay = 0;
    public float waypointGizmoRadius = 1f;

    /// <summary>Current target index</summary>
    int index;

    IAstarAI agent;
    float switchTime = float.PositiveInfinity;

    public bool hasSpottedPlayer = false;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<IAstarAI>();
    }

    /// <summary>Update is called once per frame</summary>
    void Update()
    {
        if (!hasSpottedPlayer)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (targets.Length == 0) return;

        bool search = false;

        // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
        // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
        if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime))
        {
            switchTime = Time.time + delay;
        }


        index = index % targets.Length;
        agent.destination = targets[index].position;

        if (search) agent.SearchPath();
    }

    private void OnDrawGizmos()
    {
        foreach (Transform t in targets)
        {
            Gizmos.DrawWireSphere(t.transform.position, waypointGizmoRadius);
            Gizmos.color = Color.black;
        }
    }
}
