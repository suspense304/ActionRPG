using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public static PlayerSoundManager instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource themeSource;
    [SerializeField] AudioClip playerHit;
    [SerializeField] AudioClip swordAttack;
    [SerializeField] AudioClip itemFound;

    void Awake()
    {
        instance = this;
    }
    public void PlayHitSound()
    {
        audioSource.Stop();
        audioSource.clip = playerHit;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.Stop();
        audioSource.clip = swordAttack;
        audioSource.Play();
    }

    public void PlayItemFound()
    {
        audioSource.Stop();
        audioSource.clip = itemFound;
        audioSource.Play();
        //StartCoroutine(RestartThemeMusic());
    }

    public void PauseThemeMusic()
    {
        themeSource.Pause();
    }

    public void PlayThemeMusic()
    {
        themeSource.Play();
    }

    IEnumerator RestartThemeMusic()
    {
        yield return new WaitForSeconds(1.5f);
        themeSource.volume = .5f;
    }
}
