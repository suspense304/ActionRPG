using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoad : MonoBehaviour
{
    public PlayerStats player;

    void Awake()
    {
        player = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>();
        player.UpdateStats();
    }

}
