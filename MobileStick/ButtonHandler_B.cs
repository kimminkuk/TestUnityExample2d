using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler_B : MonoBehaviour
{
    // Start is called before the first frame update

    public bool attackbutton_B;

    public void PointerDown()
    {
        StartCoroutine( OnAttackTime_B() );
    }
    
    public void PointerUp()
    {
        StartCoroutine(OffAttackTime_B());
    }

    private IEnumerator OnAttackTime_B()
    {
        attackbutton_B = true;
        yield return null;
        attackbutton_B = false;
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator OffAttackTime_B()
    {
        attackbutton_B = false;
        yield return null;
    }
}
