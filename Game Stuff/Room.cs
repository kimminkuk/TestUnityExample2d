using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public pot[] pots;

    public virtual void OnTriggerEnter2D(Collider2D ohter)
    {
        if(ohter.CompareTag("Player") && !ohter.isTrigger)
        {
            //Activate all enemies and pots
            for(int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for(int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D ohter)
    {
        if (ohter.CompareTag("Player") && !ohter.isTrigger)
        {
            //De-activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
        }
    }
    void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
}
