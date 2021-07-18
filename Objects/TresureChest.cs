using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TresureChest : Interactable
{
    [Header("Contents")]
    public PlayerState currentState;
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signal and Dialog")]
    public Signal raiseItem;
    public GameObject diaglogBox;
    public Text dialogText;

    [Header("Animator")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if(isOpen)
        {
            anim.SetBool("opend", true);
        }
    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID //for Mobile
         if (Btn_Interactable.interactable_button && playerInRange)
         //if(buttonHandler.interactable_button && playerInRange )
         {
             if (!isOpen)
             {
                 Debug.Log("OpenChest\n");
                 //Open the chest
                 OpenChest();
             } 
             else
             {
                 if(Btn_Interactable2.interactable_button2) 
                 {
                     //Chest is already open
                     Debug.Log("ChestAlreadyOpen\n");
                     ChestAlreadyOpen();
                 }
             }
         }

        //if (Btn_Interactable.interactable_button && playerInRange)
        ////if(buttonHandler.interactable_button && playerInRange )
        //{
        //    if (!isOpen)
        //    {
        //        //Open the chest
        //        Debug.Log("OpenChest\n");
        //        OpenChest();
        //    }
        //    else
        //    {
        //        //Chest is already open
        //        Debug.Log("ChestAlreadyOpen\n");
        //        ChestAlreadyOpen();
        //    }
        //}
#else //for PC
        //        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (!isOpen)
            {
                //Open the chest
                Debug.Log("OpenChest\n");
                OpenChest();
            }
            else
            {
                //Chest is already open
                Debug.Log("ChestAlreadyOpen\n");
                ChestAlreadyOpen();
            }
        }
#endif
    }

    public override void OpenChest_Mobile()
    {
        if (playerInRange)
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
            storedOpen.RuntimeValue = true;
        }
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
        storedOpen.RuntimeValue = true;
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
            Btn_Interactable.interactable_button = false;
            //contextOn.Raise();
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (!isOpen)
            {
                context.Raise();
            }
            //contextOff.Raise();
            playerInRange = false;
        }
    }
}
