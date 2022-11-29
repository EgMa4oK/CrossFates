using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CrossFates
{
    public abstract class Condition : ScriptableObject
    {
        public abstract event Action onPerform;
        public abstract bool Performed { get; protected set; }

        private void OnEnable()
        {
            Crutch.onRestart += Restart;
        }
        private void OnDisable()
        {
            Crutch.onRestart -= Restart;
        }

        private void Restart()
        {
            Performed = false;
        }
    }
}
