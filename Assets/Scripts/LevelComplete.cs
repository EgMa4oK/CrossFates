using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public class LevelComplete : MonoBehaviour, IPauseRequster
    {
        [SerializeField] GameObject _menu;

        public void Complete()
        {
            Pause.Request(this);
            InputManager.Input.Disable();
            _menu.SetActive(true);
        }
    }
}
