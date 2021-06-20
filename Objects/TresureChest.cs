using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TresureChest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject diaglogBox;
    public Text dialogText;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
#if false //for Mobile

#else //for PC
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if(!isOpen)
            {
                //Open the chest
                OpenChest();
            }
            else
            {
                //Chest is already open
                ChestAlreadyOpen();
            }
        }
#endif
    }

    public void OpenChest()
    {
        // Dialog Window on
        diaglogBox.SetActive(true);
        // dialog text = contest text
        dialogText.text = contents.itemDescription;
        // add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // Raise the signal to the player to animate
        raiseItem.Raise();
        // raise the context clue
        context.Raise();
        // Set the chest to opened
        isOpen = true;
        anim.SetBool("opend", true);
    }
    public void ChestAlreadyOpen()
    {
        // Dialog off
        diaglogBox.SetActive(false);
        // raise the signal to the player to stop animating
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            //contextOn.Raise();
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            //contextOff.Raise();
            playerInRange = false;
        }
    }
}