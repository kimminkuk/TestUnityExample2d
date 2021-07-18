using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn_Interactable : MonoBehaviour
{
    public bool interactable_button;
    public Btn_Interactable2 tmp;
    public bool testSign;
    public void PointerDown()
    {
        if (!interactable_button)
        {
            interactable_button = true;
            tmp.interactable_button2 = false;
            Debug.Log("interactable_button = true;\n");
        }
        else
        {
            interactable_button = false;
            tmp.interactable_button2 = false;
            Debug.Log("interactable_button = false;\n");
        }
    }

    public void PointerUp()
    {
        //StartCoroutine(OnAttackTime_B());
        if (!interactable_button)
        {
            interactable_button = true;
            tmp.interactable_button2 = false;
            Debug.Log("interactable_button = true;\n");
        }
        else
        {
            interactable_button = false;
            tmp.interactable_button2 = false;
            Debug.Log("interactable_button = false;\n");
        }
    }

    public void TestButtonInput()
    {
        if(!testSign)
        {
            testSign = true;
        }
        else
        {
            testSign = false;
        }
    }

    private IEnumerator OnAttackTime_B()
    {
        interactable_button = true;
        tmp.interactable_button2 = false;
        yield return null;
        interactable_button = false;
        tmp.interactable_button2 = false;
        yield return new WaitForSeconds(0.3f);
    }
}
