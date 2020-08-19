using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Consumable
{
    public int healthAmount;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            player.IncreaseHealth(healthAmount);
            audio.PlayPickedUp();
            Destroy(gameObject);
        }
    }

}
