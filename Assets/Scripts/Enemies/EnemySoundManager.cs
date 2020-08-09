using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip enemyHit;
    [SerializeField] AudioClip enemyAttack;
    [SerializeField] AudioClip enemyDeath;


    void Awake()
    {
        audio = GameObject.FindWithTag("Manager").GetComponent<AudioSource>();
    }
    public void EnemyHitSound()
    {
        audio.Stop();
        audio.clip = enemyHit;
        audio.Play();
    }

    public void EnemyAttackSound()
    {
        audio.Stop();
        audio.clip = enemyAttack;
        audio.Play();
    }

    public void EnemyDeathSound()
    {
        audio.Stop();
        audio.clip = enemyDeath;
        audio.Play();
    }
}
