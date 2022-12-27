using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace CrossFates
{
    public class Menu : MonoBehaviour, IPauseRequster
    {
        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _character;
        private Controls _controls;
        private void Awake()
        {
            _controls = new Controls();
            _menu.SetActive(false);    
        }
        private void OnEnable()
        {

            _controls.Enable();
            _controls.Character.Menu.performed += ChangeMenuState;
        }
        private void OnDisable()
        {

            _controls.Disable();
            _controls.Character.Menu.performed -= ChangeMenuState;
        }

        public void ChangeMenuState(InputAction.CallbackContext callback)
        {
            _menu.SetActive(!_menu.activeSelf);
            if (_menu.activeSelf)
            {
                Pause.Request(this);
                print(_character.activeSelf);
                if (!_character.activeSelf)
                {
                    _controls.Disable();
                }
            }
            else
            {
                Pause.Stop(this);
            }
        }

        public void Restart()
        {
            Time.timeScale = 1;
            LevelManager.LoadLevel(LevelManager.Level);
        }

        public void Exit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
