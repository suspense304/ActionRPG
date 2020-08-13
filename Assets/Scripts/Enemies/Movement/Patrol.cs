using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MovementType
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public float roundingDistance;

    public Enemy enemyInstance;

    public Vector3[] localWayPoints;
    public Vector3[] globalWayPoints;
    public bool cyclic;

    [Range(0, 2)]
    public float easeAmount;
    public float waitTime;

    int fromWaypointIndex;
    float nextMoveTime;
    float percentBetweenWaypoints;

    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        globalWayPoints = new Vector3[localWayPoints.Length];
        for (int i = 0; i < localWayPoints.Length; i++)
        {
            globalWayPoints[i] = localWayPoints[i] + transform.position;
        }
    }

    public void CheckDistance()
    {
        if (enemyInstance.currentState == EnemyState.stagger) return; 

        if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
            Vector3.Distance(target.position, transform.position) > attackRadius &&
            enemyInstance.currentState != EnemyState.stagger)
        {
            // Chase Movement

            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, enemyInstance.moveSpeed * Time.deltaTime);
            enemyInstance.ChangeAnim(temp - transform.position);
            enemyInstance.rb.MovePosition(temp);
            enemyInstance.anim.SetBool("isActive", true);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            // Patrol Movement
            Vector3 temp = CalculateMovement();
            enemyInstance.ChangeAnim(transform.position - globalWayPoints[fromWaypointIndex]);
            transform.Translate(temp);
            enemyInstance.anim.SetBool("isActive", true);
        }
    }

    Vector3 CalculateMovement()
    {
        if (Time.time < nextMoveTime)
        {
            return Vector3.zero;
        }

        fromWaypointIndex %= globalWayPoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWayPoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWayPoints[fromWaypointIndex], globalWayPoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * enemyInstance.moveSpeed / distanceBetweenWaypoints;
        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = EaseIn(percentBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(globalWayPoints[fromWaypointIndex], globalWayPoints[toWaypointIndex], easedPercentBetweenWaypoints);

        if (percentBetweenWaypoints >= 1)
        {
            percentBetweenWaypoints = 0;
            fromWaypointIndex++;

            if (!cyclic)
            {
                if (fromWaypointIndex >= globalWayPoints.Length - 1)
                {
                    fromWaypointIndex = 0;
                    Array.Reverse(globalWayPoints);
                }
            }

            nextMoveTime = Time.time + waitTime;

        }

        return newPos - transform.position;
    }

    float EaseIn(float x)
    {
        float a = easeAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    void OnDrawGizmos()
    {
        if (localWayPoints != null)
        {
            Gizmos.color = Color.red;
            float size = 0.3f;

            for (int i = 0; i < localWayPoints.Length; i++)
            {
                Vector3 globalWayPointPos = (Application.isPlaying) ? globalWayPoints[i] : localWayPoints[i] + transform.position;
                Gizmos.DrawLine(globalWayPointPos - Vector3.up * size, globalWayPointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWayPointPos - Vector3.left * size, globalWayPointPos + Vector3.left * size);
            }

        }
    }


}
