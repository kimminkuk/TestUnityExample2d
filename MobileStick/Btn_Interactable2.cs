using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Interactable2 : MonoBehaviour
{
    public bool interactable_button2;
    public void PointerDown()
    {

    }

    public void PointerUp()
    {
        //StartCoroutine(OnAttackTime_B());
        if (!interactable_button2)
        {
            interactable_button2 = true;
            Debug.Log("interactable_button2 = true;\n");
        }
        else
        {
            interactable_button2 = true;
            Debug.Log("interactable_button2 = false;\n");
        }
    }

    private IEnumerator OnAttackTime_B()
    {
        interactable_button2 = true;
        yield return null;
        interactable_button2 = false;
        yield return new WaitForSeconds(0.3f);
    }
}
