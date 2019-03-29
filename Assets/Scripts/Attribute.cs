using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeType
{
    Damage,
    Defense,
    Strength,
    Health,
    Intelligence,
    Mana,
    Agility,
    DodgeChance,
    CriticalRate
}

public class Attribute
{
    private float baseValue;
    private readonly List<StatModifier> statModifiers;

    private float lastValue;
    public bool Recaliberate = true;

    /// <summary>
    /// Constructor. Takes base value of the stat as paramater.
    /// </summary>
    public Attribute(float baseValue)
    {
        this.baseValue = baseValue;
        this.statModifiers = new List<StatModifier>();
    }

    /// <summary>
    /// Returns recalculated stats after applying modifications.
    /// Depends on StatModifier list being sorted by order.
    /// </summary>
    private float ValueAfterModifications()
    {
        float newValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            switch (statModifiers[i].Type)
            {
                case StatModifierType.FlatAdd:
                    newValue += statModifiers[i].Value;
                    break;
                case StatModifierType.PercentMult:
                    newValue *= 1 + statModifiers[i].Value;
                    break;
                case StatModifierType.PercentAdd:
                    sumPercentAdd += statModifiers[i].Value;
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModifierType.PercentAdd)
                        newValue *= 1 + sumPercentAdd;
                    break;
            }
        }

        return (float)Math.Round(newValue, 4);
    }

    /// <summary>
    /// Compares orders of two StatModifier objects.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        return a.Order - b.Order;
    }

    /// <summary>
    /// Adds a modifier to StatModifier list.
    /// </summary>
    /// <param name="modifier"></param>
    public void AddModifier(StatModifier modifier)
    {
        Recaliberate = true;
        statModifiers.Add(modifier);
        statModifiers.Sort(CompareModifierOrder);
    }

    /// <summary>
    /// Removes a modifier to StatModifier list.
    /// </summary>
    /// <param name="modifier"></param>
    /// <returns></returns>
    public bool RemoveModifier(StatModifier modifier)
    {
        if (statModifiers.Remove(modifier))
        {
            Recaliberate = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes all modifiers from a given source.
    /// </summary>
    /// <param name="source"></param>
    /// <returns>Returns true if at least one modifier was removed.</returns>
    public bool RemoveAllModifiersFromSource(object source)
    {
        bool removedModifier = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                Recaliberate = true;
                removedModifier = true;
                statModifiers.RemoveAt(i);
            }
        }
        return removedModifier;
    }

    /// <summary>
    /// Value before any modifications have been applied.
    /// </summary>
    public float BaseValue
    {
        get { return baseValue; }
        set
        {
            baseValue = value;
            Recaliberate = true;
        }
    }

    /// <summary>
    /// Value after all modifications have been applied.
    /// </summary>
    public float Value
    {
        get
        {
            if (Recaliberate)
            {
                lastValue = ValueAfterModifications();
                Recaliberate = false;
            }
            return lastValue;
        }
    }

    public Attribute GetClone()
    {
        return (Attribute)this.MemberwiseClone();
    }
}
