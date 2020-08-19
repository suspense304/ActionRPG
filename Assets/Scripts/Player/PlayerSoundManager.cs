#pragma warning disable CS0649

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
    [SerializeField] AudioClip itemPickedUp;
    [SerializeField] AudioClip spinAttack;

    void Awake()
    {
        instance = this;
    }

    public void GetReferences()
    {
        themeSource = GameObject.FindWithTag("MainCam").GetComponent<AudioSource>();
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


    public void PlaySpinAttackSound()
    {
        audioSource.Stop();
        audioSource.clip = spinAttack;
        audioSource.Play();
    }

    public void PlayItemFound()
    {
        audioSource.Stop();
        audioSource.clip = itemFound;
        audioSource.Play();
        StartCoroutine(RestartThemeMusic());
    }

    public void PlayPickedUp()
    {
        audioSource.Stop();
        audioSource.clip = itemPickedUp;
        audioSource.Play();
    }

    public void PauseThemeMusic()
    {
        GetReferences();
        themeSource.Pause();
    }

    public void PlayThemeMusic()
    {
        GetReferences();
        themeSource.Play();
    }

    IEnumerator RestartThemeMusic()
    {
        GetReferences();
        yield return new WaitForSeconds(1.5f);
        themeSource.volume = .2f;
    }
}
