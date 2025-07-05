using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fundae.Enemy
{
    using Player;

    public class EnemyFly2D : MonoBehaviour
    {
        public Color restColor;
        public Color attackColor;
        public float speed;
        public float oscilation;
        public float minAttackTime, maxAttackTime;
        public float attackDistance = 5.0f;
        public List<Vector2> waypoints;
        public GameObject player;
        public SpriteRenderer spriteRenderer;
        public bool isResting = true;

        private Vector2 lastPosition;
        private int positionIndex;
        private bool isMoving = false;
        private Coroutine movementCoroutine;

        private void Awake()
        {
            StartCoroutine(Cycle());
        }

        public void Dead()
        {
            if (isResting)
            {
                StopAllCoroutines();
                spriteRenderer.color = Color.white;
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Dead();
        }

        private IEnumerator Cycle()
        {
            isResting = true;
            float attackTime = Random.Range(minAttackTime, maxAttackTime);
            float number = 0.0f;
            while (number < attackTime)
            {
                if (!isMoving)
                {
                    if (movementCoroutine != null)
                    {
                        StopCoroutine(movementCoroutine);
                    }
                    movementCoroutine = StartCoroutine(Movement(NextPositionIndex(positionIndex)));
                }
                number += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (movementCoroutine != null)
            {
                StopCoroutine(movementCoroutine);
                movementCoroutine = null;
                isMoving = false;
            }
            lastPosition = transform.position;
            StartCoroutine(Attack());
        }

        private IEnumerator Movement(int newPosition)
        {
            isMoving = true;
            float number = 0.0f;
            Vector2 basePosition;
            Vector2 oscillationOffset;
            while (Vector2.Distance(transform.position, waypoints[newPosition]) > 0.3f)
            {
                basePosition = Vector2.Lerp(waypoints[positionIndex], waypoints[newPosition], number);
                oscillationOffset = Vector2.up * Mathf.Sin(number * Mathf.PI * 2) * oscilation;
                transform.position = basePosition + oscillationOffset; 
                number += Time.deltaTime / speed;
                yield return new WaitForEndOfFrame();
            }
            positionIndex = newPosition;
            isMoving = false;
        }

        private IEnumerator Attack()
        {
            isResting = false;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(1.0f);
            Vector2 playerDirection = (player.transform.position - transform.position).normalized;
            Vector2 positionToAppear = (Vector2)player.transform.position - playerDirection * attackDistance;
            Vector2 playerPosition = player.transform.position;
            transform.position = positionToAppear;
            spriteRenderer.color = attackColor;
            spriteRenderer.enabled = true;
            while (Vector2.Distance(transform.position, playerPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, player.transform.position) < 0.5f)
                {
                    player.GetComponent<BasicPlayer2D>().Dead();
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(1.0f);
            transform.position = lastPosition;
            spriteRenderer.color = restColor;
            spriteRenderer.enabled = true;
            StartCoroutine(Cycle());
        }
        
        private int NextPositionIndex(int currentIndex)
        {
            currentIndex++;
            return currentIndex >= waypoints.Count? 0 : currentIndex;
        }

#if UNITY_EDITOR

        public Transform objectReader;

        private void ReadPosition ()
        {
            waypoints.Add(objectReader.position);
        }

        [CustomEditor(typeof(EnemyFly2D))]
        private class EnemyFly2DEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                EnemyFly2D enemy = (EnemyFly2D)target;
                if(GUILayout.Button("Read Position"))
                {
                    enemy.ReadPosition();
                }
            }
        }

#endif
    }
}



