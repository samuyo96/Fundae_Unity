using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fundae.Player
{
    public class BasicPlayerAnim : BasicPlayer
    {
        public Animator animator;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            animator.SetFloat("Movement", Mathf.Abs(Input.GetAxis("Vertical")));
        }
    }
}

