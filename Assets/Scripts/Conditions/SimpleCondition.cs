using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCondition : MonoBehaviour, IPerformable
{
    public bool Performed => throw new NotImplementedException();

    public event Action OnPerformed;

}
