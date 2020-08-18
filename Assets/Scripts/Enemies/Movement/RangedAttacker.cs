using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttacker : MovementType
{
    public GameObject target;
    public GameObject projectile;
    public float fireRadius;
    public float fireRate;
    public Enemy enemyInstance;

    public Vector3 homePosition;
    public float boundary;

    float shotTimer;
    bool canFire;

    void Start()
    {
        homePosition = transform.position;
        target = GameObject.FindWithTag("Player");
        enemyInstance.currentState = EnemyState.idle;
    }

    void Update()
    {
        if(shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }

        if(shotTimer <= 0)
        {
            canFire = true;
        }
    }

    public void CheckDistance()
    {
        if (enemyInstance.currentState == EnemyState.stagger) return;

        if (Vector3.Distance(homePosition, target.transform.position) <= boundary)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= fireRadius  
                    &&  enemyInstance.currentState != EnemyState.stagger 
                    && target.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
            {

                if(canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject go = Instantiate(projectile, transform.position, Quaternion.identity);
                    go.GetComponent<Projectile>().Fire(tempVector);
                    enemyInstance.ChangeState(EnemyState.walk);
                    enemyInstance.anim.SetBool("isActive", true);
                    shotTimer = fireRate;
                    canFire = false;
                }
                
            }
            else if (Vector3.Distance(target.transform.position, transform.position) > fireRadius)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.transform.position, enemyInstance.moveSpeed * Time.deltaTime);
                enemyInstance.ChangeAnim(temp - transform.position);
                enemyInstance.rb.MovePosition(temp);
                enemyInstance.ChangeState(EnemyState.walk);
                enemyInstance.anim.SetBool("isActive", true);
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
                }
                else
                {
                    enemyInstance.anim.SetBool("isActive", false);
                    enemyInstance.ChangeState(EnemyState.idle);
                }
            }
        }
    }
}
