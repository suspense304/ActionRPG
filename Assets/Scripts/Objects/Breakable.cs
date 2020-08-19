using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    Animator anim;
    BoxCollider2D boxCollider;

    public AudioSource audio;
    public AudioClip clip;

    [Header("Loot Table")]
    public LootTable breakableLoot;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audio = GameObject.FindWithTag("Manager").GetComponent<PlayerSoundManager>().GetComponent<AudioSource>(); ;
    }

    void MakeLoot()
    {
        if (breakableLoot != null)
        {
            GameObject current = breakableLoot.LootItem();
            if (current != null)
            {
                Instantiate(current, transform.position, Quaternion.identity);
            }
        }
    }

    public void Break()
    {
        anim.SetBool("isBroken", true);
        MakeLoot();
        audio.Stop();
        audio.clip = clip;
        audio.Play();
        boxCollider.enabled = false;
    }
}
