using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool intialValue;

    [HideInInspector]
    public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = intialValue;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
