using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string conditionalSourceField = "";
    public bool hideInInspector = false;
    public bool inverse = false;

    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.conditionalSourceField = conditionalSourceField;
        this.hideInInspector = false;
        this.inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        this.conditionalSourceField = conditionalSourceField;
        this.hideInInspector = hideInInspector;
        this.inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
    {
        this.conditionalSourceField = conditionalSourceField;
        this.hideInInspector = hideInInspector;
        this.inverse = inverse;
    }
}