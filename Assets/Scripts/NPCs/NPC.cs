using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Fundae.NPCFunctions
{
    public class NPC : MonoBehaviour
    {
        public NavMeshAgent agent;

        protected void AgentMovement(Vector3 newPos)
        {
            agent.SetDestination(newPos);
        }
    }
}
