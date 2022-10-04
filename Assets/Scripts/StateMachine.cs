using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CrossFates
{
    public interface IStateSwitcher
    {
        public void SwitchState<T>() where T : BaseState;

    }

}