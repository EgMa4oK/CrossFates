using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace CrossFates
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
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

        private void ChangeMenuState(InputAction.CallbackContext callback)
        {
            print("asd");
            _menu.SetActive(!_menu.activeSelf);
            Time.timeScale = _menu.activeSelf ? 0 : 1;
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
