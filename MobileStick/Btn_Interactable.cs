using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn_Interactable : MonoBehaviour
{
    public bool interactable_button;
    public void PointerDown()
    {
        interactable_button = true;
    }

    public void PointerUp()
    {
        interactable_button = false;
    }
}
