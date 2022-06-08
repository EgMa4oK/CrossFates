using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexCondition : IPerformable
{
    public bool Performed => throw new NotImplementedException();
    public List<IPerformable> Conditions;

    public event Action OnPerformed;


}
