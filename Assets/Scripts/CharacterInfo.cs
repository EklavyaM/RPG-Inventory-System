using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private Text characterName;
    [SerializeField] private Text characterDescription;
    [SerializeField] private Text characterDamage;
    [SerializeField] private Text characterDefense;
    [SerializeField] private Text characterAgility;
    [SerializeField] private Text characterIntelligence;
    [SerializeField] private Text characterStrength;
    [SerializeField] private Text characterHealth;
    [SerializeField] private Text characterMana;
    [SerializeField] private Text characterDodgeChance;
    [SerializeField] private Text characterCriticalRate;


    /// <summary>
    /// Displays Character Stats.
    /// </summary>
    /// <param name="attributes"></param>
    public void ShowCharacterInfo(Dictionary<AttributeType, Attribute> attributes)
    {
        if (attributes == null) return;

        if (attributes.ContainsKey(AttributeType.Damage)) characterDamage.text = attributes[AttributeType.Damage].Value.ToString();
        if (attributes.ContainsKey(AttributeType.Defense)) characterDefense.text = attributes[AttributeType.Defense].Value.ToString();
        if (attributes.ContainsKey(AttributeType.Agility)) characterAgility.text = attributes[AttributeType.Agility].Value.ToString();
        if (attributes.ContainsKey(AttributeType.Intelligence)) characterIntelligence.text = attributes[AttributeType.Intelligence].Value.ToString();
        if (attributes.ContainsKey(AttributeType.Strength)) characterStrength.text = attributes[AttributeType.Strength].Value.ToString();
        if (attributes.ContainsKey(AttributeType.Health)) characterHealth.text = attributes[AttributeType.Health].Value.ToString();
        if (attributes.ContainsKey(AttributeType.Mana)) characterMana.text = attributes[AttributeType.Mana].Value.ToString();
        if (attributes.ContainsKey(AttributeType.DodgeChance)) characterDodgeChance.text = attributes[AttributeType.DodgeChance].Value.ToString();
        if (attributes.ContainsKey(AttributeType.CriticalRate)) characterCriticalRate.text = attributes[AttributeType.CriticalRate].Value.ToString();
    }

    public string CharacterName
    {
        set
        {
            this.characterName.text = (value == null || value.Length == 0) ? "Character Name" : value;
        }
    }

    public string CharacterDescription
    {
        set
        {
            this.characterDescription.text = (value == null || value.Length == 0) ? "Character Description" : value;
        }
    }
}
