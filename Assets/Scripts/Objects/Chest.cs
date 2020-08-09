using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    public bool isOpen;
    public Inventory playerInventory;
    public Item contents;
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButtonDown("interact") && isInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestEmpty();
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            isInRange = true;
            context.Raise();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            isInRange = false;
            context.Raise();
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(!dialogBox.activeInHierarchy);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        anim.SetBool("isOpen", true);
    }

    public void ChestEmpty()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }
}
