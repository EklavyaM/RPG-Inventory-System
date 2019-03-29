using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    [SerializeField] private AttributeConfig attributeConfig;
    [SerializeField] private CharacterInfo characterInfo;

    private BaseAttribute baseAttribute;
    private Dictionary<AttributeType, Attribute> attributes;

    private void Awake()
    {
        SetBaseAttributes();
    }

    /// <summary>
    /// Applies all status effects provided by the given item.
    /// Assumes that items can only buff damage, defense,
    /// strength, intelligence, and agility and the rest of the stats
    /// like health, mana, dodge chance and critical rate depend on 
    /// one of these.
    /// </summary>
    /// <param name="item"></param>
    public void ApplyStatusEffects(Item item)
    {
        if (attributes == null) return;

        if (attributes.ContainsKey(AttributeType.Damage)) attributes[AttributeType.Damage].AddModifier(new StatModifier(item.damage, StatModifierType.FlatAdd, item));
        if (attributes.ContainsKey(AttributeType.Defense)) attributes[AttributeType.Defense].AddModifier(new StatModifier(item.defence, StatModifierType.FlatAdd, item));

        if (attributes.ContainsKey(AttributeType.Strength))
        {
            attributes[AttributeType.Strength].AddModifier(new StatModifier(item.strength, StatModifierType.FlatAdd, item));
            if (attributes.ContainsKey(AttributeType.Health))
                attributes[AttributeType.Health].BaseValue = attributes[AttributeType.Strength].Value * 12;
        }

        if (attributes.ContainsKey(AttributeType.Intelligence))
        {
            attributes[AttributeType.Intelligence].AddModifier(new StatModifier(item.intel, StatModifierType.FlatAdd, item));
            if (attributes.ContainsKey(AttributeType.Mana))
                attributes[AttributeType.Mana].BaseValue = attributes[AttributeType.Intelligence].Value * 14;
        }

        if (attributes.ContainsKey(AttributeType.Agility))
        {
            attributes[AttributeType.Agility].AddModifier(new StatModifier(item.agility, StatModifierType.FlatAdd, item));
            if (attributes.ContainsKey(AttributeType.DodgeChance))
                attributes[AttributeType.DodgeChance].BaseValue = attributes[AttributeType.Agility].Value * 0.2f;
            if (attributes.ContainsKey(AttributeType.CriticalRate))
                attributes[AttributeType.CriticalRate].BaseValue = attributes[AttributeType.Agility].Value * 0.15f;
        }

        characterInfo.ShowCharacterInfo(attributes);
    }

    /// <summary>
    /// Removes all status effects applied by the given item.
    /// Assumes that items can only buff damage, defense,
    /// strength, intelligence, and agility and the rest of the stats
    /// like health, mana, dodge chance and critical rate depend on 
    /// one of these.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveStatusEffects(Item item)
    {
        if (attributes == null) return;

        if (attributes.ContainsKey(AttributeType.Damage)) attributes[AttributeType.Damage].RemoveAllModifiersFromSource(item);
        if (attributes.ContainsKey(AttributeType.Defense)) attributes[AttributeType.Defense].RemoveAllModifiersFromSource(item);

        if (attributes.ContainsKey(AttributeType.Strength))
        {
            attributes[AttributeType.Strength].RemoveAllModifiersFromSource(item);
            if (attributes.ContainsKey(AttributeType.Health))
                attributes[AttributeType.Health].BaseValue = attributes[AttributeType.Strength].Value * 12;
        }

        if (attributes.ContainsKey(AttributeType.Intelligence))
        {
            attributes[AttributeType.Intelligence].RemoveAllModifiersFromSource(item);
            if (attributes.ContainsKey(AttributeType.Mana))
                attributes[AttributeType.Mana].BaseValue = attributes[AttributeType.Intelligence].Value * 14;
        }

        if (attributes.ContainsKey(AttributeType.Agility))
        {
            attributes[AttributeType.Agility].RemoveAllModifiersFromSource(item);
            if (attributes.ContainsKey(AttributeType.DodgeChance))
                attributes[AttributeType.DodgeChance].BaseValue = attributes[AttributeType.Agility].Value * 0.2f;
            if (attributes.ContainsKey(AttributeType.CriticalRate))
                attributes[AttributeType.CriticalRate].BaseValue = attributes[AttributeType.Agility].Value * 0.15f;
        }
        characterInfo.ShowCharacterInfo(attributes);
    }

    /// <summary>
    /// Sets base attributes of the character using a 
    /// a scriptable object of the AttributeConfig class
    /// </summary>
    public void SetBaseAttributes()
    {
        if (attributeConfig == null) return;

        baseAttribute = attributeConfig.baseAttribute;

        if (attributes == null) attributes = new Dictionary<AttributeType, Attribute>();
        attributes.Clear();

        attributes.Add(AttributeType.Damage, new Attribute(baseAttribute.damage));
        attributes.Add(AttributeType.Defense, new Attribute(baseAttribute.defense));
        attributes.Add(AttributeType.Agility, new Attribute(baseAttribute.agility));
        attributes.Add(AttributeType.Intelligence, new Attribute(baseAttribute.intelligence));
        attributes.Add(AttributeType.Strength, new Attribute(baseAttribute.strength));
        attributes.Add(AttributeType.Health, new Attribute(baseAttribute.strength * 12));
        attributes.Add(AttributeType.Mana, new Attribute(baseAttribute.intelligence * 14));
        attributes.Add(AttributeType.DodgeChance, new Attribute(baseAttribute.agility * 0.2f));
        attributes.Add(AttributeType.CriticalRate, new Attribute(baseAttribute.agility * 0.15f));

        characterInfo.CharacterName = baseAttribute.characterName;
        characterInfo.CharacterDescription = baseAttribute.characterDescription;

        characterInfo.ShowCharacterInfo(attributes);
    }

}
