using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject
{

    public float intialValue;

    //[HideInInspector]
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = intialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}
