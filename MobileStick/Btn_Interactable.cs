using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn_Interactable : MonoBehaviour
{
    [SerializeField] 
    int time;

    private bool BtnInteractable;
    public Image cooldown;
    public bool coolingDown;
    public float waitTime = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coolingDown == true)
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            Debug.Log(cooldown.fillAmount);
        }    
    }

    public void PointerDown()
    {
        StartCoroutine(Interaction());
        Debug.Log(time);
        if(time == 700)
        {
            coolingDown = true;
            Debug.Log("Ok\n");
        }
    }

    public void PointerUp()
    {
        StopCoroutine(Interaction());
        Debug.Log(time);
    }

    private IEnumerator Interaction()
    {
        yield return new WaitForSeconds(time);
    }
}
