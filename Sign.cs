using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject diaglogBox;
    public Text diaglogText;
    public string diaglog;
    public bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if false //for Mobile

#else //for PC
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            diaglogBox.SetActive(false);
        }
    }

}
