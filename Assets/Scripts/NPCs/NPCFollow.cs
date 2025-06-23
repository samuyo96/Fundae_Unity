using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fundae.NPCFunctions
{
    public class NPCFollow : NPC
    {
        public Transform player;

        private void FixedUpdate()
        {
            if (Wait())
            {
                AgentMovement(player.position);
                return;
            }
            agent.ResetPath();
        }

        private bool Wait()
        {
            return Vector3.Distance(player.position, transform.position) > 1.5f;
        }
    }
}


