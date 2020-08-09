using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    Animator anim;
    Rigidbody2D rb;
    Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyState.idle;
    }
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
            Vector3.Distance(target.position, transform.position) > attackRadius &&
            currentState != EnemyState.stagger)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnim(temp - transform.position);
            rb.MovePosition(temp);
            ChangeState(EnemyState.walk);
            anim.SetBool("isActive", true);
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("isActive", false);
        }
    }

    void SetAnimationFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    void ChangeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimationFloat(Vector2.right);
            } 
            else if(direction.x < 0)
            {
                SetAnimationFloat(Vector2.left);
            }
        } 
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimationFloat(Vector2.up);

            }
            else if (direction.y < 0)
            {
                SetAnimationFloat(Vector2.down);
            }
        }
    }

    void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
