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
        //if (!attackbutton)
        //{
        //    attackbutton = true;
        //}
        StartCoroutine(OnAttackTime());
    }

    public void PointerUp()
    {
        //    attackbutton = false;
        StartCoroutine(OffAttackTime());
    }

    private IEnumerator OnAttackTime()
    {
        attackbutton = true;
        yield return null;
        attackbutton = false;
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator OffAttackTime()
    {
        attackbutton = false;
        yield return null;
    }
}
