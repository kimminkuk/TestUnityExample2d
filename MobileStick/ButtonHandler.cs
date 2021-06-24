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
        if (!attackbutton)
        {
            attackbutton = true;
        }
    }

    public void PointerUp()
    {
        attackbutton = false;
    }

    private IEnumerator OnAttackTime()
    {
        attackbutton = true;
        yield return null;
    }

    private IEnumerator OffAttackTime()
    {
        attackbutton = false;
        yield return null;
    }
}
