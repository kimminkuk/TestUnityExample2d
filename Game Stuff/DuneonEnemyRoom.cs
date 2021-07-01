using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuneonEnemyRoom : DungeonRoom
{
    public Door[] doors;
    private int door_call;
    public void CheckEnemies()
    {
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    //if (enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1)
        //    //{            
        //    //    Debug.Log("CheckEnemies() return\n");
        //    //    return;
        //    //}
        //        OpenDoors();
        //    return;
        //}
        
        //Temp Code for Dungeon Enemy Room Open
        door_call++;
        if(enemies.Length == door_call)
        {
            OpenDoors();
        }
    }

    public override void OnTriggerEnter2D(Collider2D ohter)
    {
        if (ohter.CompareTag("Player") && !ohter.isTrigger)
        {
            //Activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            CloseDoors();
            virtualCamera.SetActive(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D ohter)
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
            virtualCamera.SetActive(false);
        }
    }

    public void CloseDoors()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            Debug.Log("OpenDoors Enemy Door()\n");
            doors[i].Open();
        }
    }
}
