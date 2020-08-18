using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinProjectile : Projectile
{
    public float fireRate;

    float shotTimer;
    void Start()
    {
        shotTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
