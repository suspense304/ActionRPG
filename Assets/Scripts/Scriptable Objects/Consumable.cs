using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public PlayerStats player;
    public PlayerSoundManager audio;

    void Awake()
    {
        player = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>();
        audio = player.GetComponent<PlayerSoundManager>();
    }
}
