using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Consumable
{
    public int goldAmount;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            player.ChangeGold(goldAmount);
            audio.PlayPickedUp();
            Destroy(gameObject);
        }
    }
}
