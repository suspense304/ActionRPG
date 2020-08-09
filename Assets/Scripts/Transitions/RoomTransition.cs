using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    public GameObject virtualCam;
    public bool isNewArea;
    public string areaName;
    public GameObject text;
    public Text placeText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            if (isNewArea && text != null)
            {
                text.SetActive(true);
                placeText.text = areaName;
                StartCoroutine(FadeInText(2f));
            }
        }

        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCam.SetActive(false);
        }
    }

    public IEnumerator FadeInText(float t)
    {
        placeText.color = new Color(placeText.color.r, 
            placeText.color.g, 
            placeText.color.b, 0);
        while (placeText.color.a < 1.0f)
        {
            placeText.color = new Color(placeText.color.r, 
                placeText.color.g, 
                placeText.color.b, 
                placeText.color.a + (Time.deltaTime / t));
            yield return null;
        }
        StartCoroutine(FadeOutText(2f));
    }

    public IEnumerator FadeOutText(float t)
    {
        placeText.color = new Color(placeText.color.r, 
            placeText.color.g, 
            placeText.color.b, 1);
        while (placeText.color.a > 0.0f)
        {
            placeText.color = new Color(placeText.color.r, 
                placeText.color.g,
                placeText.color.b,
                placeText.color.a - (Time.deltaTime / t));
            yield return null;
        }
        text.SetActive(false);
    }
}
