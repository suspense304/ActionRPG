using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialPosition;
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        initialPosition = defaultValue;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
