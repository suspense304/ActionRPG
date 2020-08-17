#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    idle, 
    walk,
    attack,
    stagger,
    defend
}
public class Enemy : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] float attackDelay;
    [SerializeField] string enemyName;
    [SerializeField] public int baseAttack;
    [SerializeField] public float moveSpeed;
    [SerializeField] int XP;
    [SerializeField] GameObject deathEffect;
    [SerializeField] EnemySoundManager enemySoundManager;
    [SerializeField] MovementType movementType;

    public Animator anim;
    public Rigidbody2D rb;

    public PlayerStats player;

    public EnemyState currentState;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        enemySoundManager.EnemyHitSound();
        CheckForDeath();
    }

    public void CheckForDeath()
    {
        if (health <= 0)
        {
            enemySoundManager.EnemyDeathSound();
            EnemyDead();
        }
    }

    public void EnemyDead()
    {
        player.AddXP(XP);
        DeathEffect();
        this.gameObject.SetActive(false);
    }

    void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject go = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(go, 1f);
        }
    }
    public void Knockback(Rigidbody2D rb, float knockbackDuration, int damage)
    {
        currentState = EnemyState.stagger;
        if(rb != null) StartCoroutine(KnockbackWait(rb, knockbackDuration));
        TakeDamage(damage);
    }

    IEnumerator KnockbackWait(Rigidbody2D rb, float knockbackDuration)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockbackDuration);
            rb.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }

    public void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimationFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimationFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
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

    void SetAnimationFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }


}
