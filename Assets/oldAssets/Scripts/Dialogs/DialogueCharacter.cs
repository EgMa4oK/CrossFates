using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{

    [SerializeField] private Dialog[] dialogs;



    public void StartDialogue()
    {
        foreach (Dialog dialog in dialogs)
        {
            if (dialog.CanStart())
            {
                dialog.Start();
                return;
            }
        } 
    }
}
