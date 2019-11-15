using System;
using UnityEngine;

public enum ComparisonType { Equals, NotEqual, GreaterThan, SmallerThan, SmallerOrEqual, GreaterOrEqual }
public enum DisablingType { ReadOnly, DontDraw }

[AttributeUsage (AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfAttribute : PropertyAttribute {
    public string name;
    public Type type;
    public object value;
    public ComparisonType comparisonType;
    public DisablingType disablingType;
    public DrawIfAttribute (string name, Type type, object value,
        ComparisonType comparisonType = ComparisonType.Equals, DisablingType disablingType = DisablingType.DontDraw) {
        this.name = name;
        this.type = type;
        this.value = value;
        this.comparisonType = comparisonType;
        this.disablingType = disablingType;
    }
}