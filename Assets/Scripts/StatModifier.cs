public enum StatModifierType
{
    FlatAdd = 100,
    PercentAdd = 200,
    PercentMult = 300
}

public class StatModifier
{
    public readonly float Value;
    public readonly StatModifierType Type;
    public readonly int Order;
    public readonly object Source;

    /// <summary>
    /// Constructor. Takes 4 arguments.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <param name="order"></param>
    /// <param name="source"></param>
    public StatModifier(float value, StatModifierType type, int order, object source)
    {
        this.Value = value;
        this.Type = type;
        this.Order = order;
        this.Source = source;
    }

    /// <summary>
    /// Constructor. Takes 2 arguments.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public StatModifier(float value, StatModifierType type) : this(value, type, (int)type, null) { }

    /// <summary>
    /// Constructor. Takes 3 arguments.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int)type, source) { }

}
