using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileDamage;
    public float projectileLife;
    public float projectileSpeed;
    public Vector2 moveDir;

    float lifeTimer;
    public Rigidbody2D rb;

    void Start()
    {
        lifeTimer = projectileLife;
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Fire(Vector2 vel)
    {
        rb.velocity = vel * projectileSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }

}
