﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearChasing : MovementType
{
    public GameObject target;
    public float chaseRadius;
    public float attackRadius;
    public Enemy enemyInstance;

    public Vector3 homePosition;
    public float boundary;

    void Start()
    {
        homePosition = transform.position;
        target = GameObject.FindWithTag("Player");
        enemyInstance.currentState = EnemyState.idle;
    }
    public void CheckDistance()
    {
        if (enemyInstance.currentState == EnemyState.stagger) return;

        if (Vector3.Distance(homePosition, target.transform.position) <= boundary)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius &&
            Vector3.Distance(target.transform.position, transform.position) > attackRadius &&
            enemyInstance.currentState != EnemyState.stagger && target.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.transform.position, enemyInstance.moveSpeed * Time.deltaTime);
                enemyInstance.ChangeAnim(temp - transform.position);
                enemyInstance.rb.MovePosition(temp);
                enemyInstance.ChangeState(EnemyState.walk);
                enemyInstance.anim.SetBool("isActive", true);
            }
            else if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius)
            {
                enemyInstance.anim.SetBool("isActive", false);
            }
        }
        else
        {
            if (Vector3.Distance(homePosition, target.transform.position) > 0)
            {
                if (Vector3.Distance(homePosition, transform.position) > 0)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, homePosition, (enemyInstance.moveSpeed * 1.5f) * Time.deltaTime);
                    enemyInstance.ChangeAnim(temp - transform.position);
                    enemyInstance.rb.MovePosition(temp);
                    enemyInstance.ChangeState(EnemyState.walk);
                    enemyInstance.anim.SetBool("isActive", true);
                } else
                {
                    enemyInstance.anim.SetBool("isActive", false);
                    enemyInstance.ChangeState(EnemyState.idle);
                }
            }
        }
    }

}
