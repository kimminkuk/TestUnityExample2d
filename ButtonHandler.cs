using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public bool attackbutton;

    public void PointerDown()
    {
        Debug.Log("GAMEBUTTON PointerDown\n");
        attackbutton = true;
    }

    public void PointerUp()
    {
        Debug.Log("GAMEBUTTON PointerUp\n");
        attackbutton = false;
    }
}
