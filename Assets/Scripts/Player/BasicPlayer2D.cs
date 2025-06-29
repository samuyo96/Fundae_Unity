using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fundae.Player
{

    public class BasicPlayer2D : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public LayerMask groundLayer;
        public Transform groundCheck;
        public float groundCheckRadius = 0.2f;

        private bool isGrounded;

        private void FixedUpdate()
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
