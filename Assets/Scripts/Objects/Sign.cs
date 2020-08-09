using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] string dialog;
    

    void Update()
    {
        if (Input.GetButtonDown("interact") && isInRange)
        {
            dialogBox.SetActive(!dialogBox.activeInHierarchy);
            dialogText.text = dialog;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            isInRange = false;
            dialogBox.SetActive(false);
            context.Raise();
        }
    }

}
