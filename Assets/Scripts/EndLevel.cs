using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFates
{
    public class EndLevel : MonoBehaviour
    {

        [SerializeField] Level _level;
        [SerializeField] Image _galock;

        private void OnEnable()
        {
            _galock.enabled = _level.IsCompleted;
        }
    }

}