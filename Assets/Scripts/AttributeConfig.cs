using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class AttributeConfig : ScriptableObject
{
    public BaseAttribute baseAttribute;
}

[Serializable]
public class BaseAttribute
{

    public string characterName;
    public string characterDescription;

    [Space]

    public float damage = 0;
    public float defense = 0;
    public float strength = 0;
    public float intelligence = 0;
    public float agility = 0;

}


