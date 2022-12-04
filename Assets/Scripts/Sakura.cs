using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    [CreateAssetMenu(menuName = "Sakura")]
    public class Sakura: ScriptableObject
    {
        public void SakuraMoment()
        {
            Application.Quit(1);
        }
    }
}
