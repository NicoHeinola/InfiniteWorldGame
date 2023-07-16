using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatPlayerStat
{
    // Base stuff
    [SerializeField] protected float baseValue = 25;

    // Getters and setters
    public float BaseValue { get { return this.baseValue; } set { this.baseValue = value; } }

    // Calculations for performance
    [SerializeField] protected float trueValue = 5;
    [SerializeField] protected float flatAddition = 0;
    [SerializeField] protected float multiply = 1;
    [SerializeField] protected float addition = 0;
    public void AddAddition(float toAdd)
    {
        this.addition += toAdd;
        this.RecalculateTrueValue();
    }
    public void AddMultiply(float toAdd)
    {
        this.multiply *= toAdd;
        this.RecalculateTrueValue();
    }
    public void AddFlatAddition(float toAdd)
    {
        this.flatAddition += toAdd;
        this.RecalculateTrueValue();
    }
    public void RemoveAddition(float toRemove)
    {
        this.addition -= toRemove;
        this.RecalculateTrueValue();
    }
    public void RemoveMultiply(float toRemove)
    {
        this.multiply /= toRemove;
        this.RecalculateTrueValue();
    }
    public void RemoveFlatAddition(float toRemove)
    {
        this.flatAddition -= toRemove;
        this.RecalculateTrueValue();
    }

    public void RecalculateTrueValue()
    {
        this.trueValue = (this.baseValue + this.addition) * this.multiply + this.flatAddition;
    }
    public float GetValue()
    {
        this.RecalculateTrueValue();
        return this.trueValue;
    }
}
[System.Serializable]
public class IntPlayerStat
{
    // Base stuff
    [SerializeField] protected int baseValue = 25;

    // Getters and setters
    public int BaseValue { get { return this.baseValue; } set { this.baseValue = value; } }

    // Calculations for performance
    [SerializeField] int trueValue = 5;
    [SerializeField] int flatAddition = 0;
    [SerializeField] float multiply = 1;
    [SerializeField] int addition = 0;
    public void AddAddition(int toAdd)
    {
        this.addition += toAdd;
        this.RecalculateTrueValue();
    }
    public void AddMultiply(float toAdd)
    {
        this.multiply *= toAdd;
        this.RecalculateTrueValue();
    }
    public void AddFlatAddition(int toAdd)
    {
        this.flatAddition += toAdd;
        this.RecalculateTrueValue();
    }
    public void RemoveAddition(int toRemove)
    {
        this.addition -= toRemove;
        this.RecalculateTrueValue();
    }
    public void RemoveMultiply(float toRemove)
    {
        this.multiply /= toRemove;
        this.RecalculateTrueValue();
    }
    public void RemoveFlatAddition(int toRemove)
    {
        this.flatAddition -= toRemove;
        this.RecalculateTrueValue();
    }

    public void RecalculateTrueValue()
    {
        this.trueValue = Mathf.RoundToInt((this.baseValue + this.addition) * this.multiply + this.flatAddition);
    }
    public virtual int GetValue()
    {
        this.RecalculateTrueValue();
        return this.trueValue;
    }
}