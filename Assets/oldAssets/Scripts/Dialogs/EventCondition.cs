using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[CreateAssetMenu(fileName = "new event", menuName = "Dialog/Conditions/Event", order = 50)]
public class EventCondition : Condition
{
    [System.NonSerialized] private bool performed = false;
    public override event Action onPerform;
    public override bool Performed
    {
        get
        {
            return performed;
        }
        protected set
        {
            if (value != performed)
            {
                performed = value;
                onPerform?.Invoke();
            }     
        }
    }
    

    public void Perform()
    {
        Performed = true;
        onPerform?.Invoke();
    }
}