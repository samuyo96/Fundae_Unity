using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Fundae.NPCFunctions
{
    public class NPCPatrol : NPC
    {
        public Vector3[] arrayPos;
        public int pos;

        private void FixedUpdate()
        {
            if (CheckDistance())
            {
                pos++;
                pos = pos >= arrayPos.Length ? 0 : pos;
                AgentMovement(arrayPos[pos]);
            }
        }

        private bool CheckDistance()
        {
            return Vector3.Distance(arrayPos[pos], transform.position) < 0.25f;
        }
    }
}


