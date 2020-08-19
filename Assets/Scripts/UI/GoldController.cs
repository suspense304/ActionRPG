using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldController : MonoBehaviour
{
    public Text text;
    public PlayerStats player;

    void Start()
    {
        player = GameObject.FindWithTag("Manager").GetComponent<PlayerStats>();
        player.playerGoldSignal.Raise();
    }
    public void UpdateGold()
    {
        text.text = player.Gold + "g";
    }
}
