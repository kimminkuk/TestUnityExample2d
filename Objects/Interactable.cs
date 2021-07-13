using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool playerInRange;
    public Signal context;
    public ButtonHandler buttonHandler;
    public Btn_Interactable Btn_Interactable;
    public Btn_Interactable2 Btn_Interactable2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            //contextOn.Raise();
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            //contextOff.Raise();
            playerInRange = false;
        }
    }

    public virtual void OpenChest_Mobile()
    {

    }
}
