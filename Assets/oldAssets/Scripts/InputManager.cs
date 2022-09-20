using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static PlayerInput input;
    public static PlayerInput Input
    {
        get
        {
            if (input == null)
            {
                input = new PlayerInput();
            }
            return input;
        }
        private set => input = value;
    }

    private void Awake()
    {
              
        DialogueManager.OnDialogueEnd += SetStatusGame;
        DialogueManager.OnDialogueStart += SetStatusMenu;
    }

    private void OnEnable()
    {
        Input.Player.Enable();
    }
    private void OnDisable()
    {
        Input.Disable();
    }

    private void SetStatusMenu()
    {
        Input.Disable();
        Input.Menu.Enable();
    }
    private void SetStatusGame()
    {
        Input.Disable();
        Input.Player.Enable();
        
        
    }
}
