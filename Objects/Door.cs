
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{ 
    key,
    enemy,
    button
}


public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    
    private void Update()
    {
#if UNITY_ANDROID 
        if (buttonHandler.attackbutton)
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                //Does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    //Remove a player key
                    playerInventory.numberOfKeys--;
                    //If so, then call the open method
                    Open();
                }
            }
        
        }
#else
        //if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetButtonDown("attack"))
        {
            if(playerInRange && thisDoorType == DoorType.key)
            {
                //Does the player have a key?
                if(playerInventory.numberOfKeys > 0)
                {
                    //Remove a player key
                    playerInventory.numberOfKeys--;
                    //If so, then call the open method
                    Open();
                }
            }
        }
#endif
    }

    public void Open()
    {
        //Turn off the door's sprite renderer
        doorSprite.enabled = false;
        //Set open to true
        open = true;
        //turn off the door's box collider
        physicsCollider.enabled = false;
    }
    public void Close()
    {
        //Turn off the door's sprite renderer
        doorSprite.enabled = true;
        //Set open to true
        open = false;
        //turn off the door's box collider
        physicsCollider.enabled = true;
    }
}
