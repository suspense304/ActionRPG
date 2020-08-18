using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockbackDuration;
    public int damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("PlayerWeapon"))
        {
            other.GetComponent<Breakable>().Break();
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                // Find distance between two objects
                Vector2 difference = hit.transform.position - transform.position;

                // Normalize distance and apply thrust
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    damage = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>().Attack;
                    other.GetComponent<Enemy>().Knockback(hit, knockbackDuration, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if(other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        if(GetComponent<Projectile>() != null)
                        {
                            damage = GetComponent<Projectile>().projectileDamage;
                        } else
                        {
                            damage = GetComponent<Enemy>().baseAttack;
                        }                        
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knockback(knockbackDuration, damage);
                    }
                }
                
            }
        }
    }


}
