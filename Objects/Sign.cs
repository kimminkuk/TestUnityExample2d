using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    //public Signal contextOn;
    //public Signal contextOff;
    public GameObject diaglogBox;
    public Text diaglogText;
    public string diaglog;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID //for Mobile
        if (buttonHandler.attackbutton && playerInRange)
        {
            if (diaglogBox.activeInHierarchy)
            {
                diaglogBox.SetActive(false);
            }
            else
            {
                diaglogBox.SetActive(true);
                diaglogText.text = diaglog;
            }
        }
#else //for PC
        //if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        if (Input.GetButtonDown("attack") && playerInRange)
        {
            if (diaglogBox.activeInHierarchy)
            {
                diaglogBox.SetActive(false);
            }
            else
            {
                diaglogBox.SetActive(true);
                diaglogText.text = diaglog;
            }
        }
#endif
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            diaglogBox.SetActive(false);
        }
    }
}
