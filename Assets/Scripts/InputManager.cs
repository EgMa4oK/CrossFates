using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace CrossFates
{
    public class InputManager : MonoBehaviour
    {
        private static Controls _input;
        public static Controls Input
        {
            get
            {
                if (_input == null)
                {
                    Input = new Controls();
                }
                return _input;
            }

            private set => _input = value;
        }
        public void Awake()
        {
            Input = new Controls();
            Input.Enable();
        }
        public static void SetStatusMenu()
        {
            Input.Disable();
            Input.Menu.Enable();
        }
        public static void SetStatusGame()
        {
            Input.Disable();
            Input.Character.Enable();


        }
    }
}
