using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrossFates
{
    public class DialogueManager : MonoBehaviour
    {
        private static DialogueManager manager;

        public static event Action OnDialogueEnd;
        public static event Action OnDialogueStart;
        public static event Action OnDialogueContinue;
        public static event Action OnDialogueChoices;

        private Story currentStory;
        private Dialog currentDialog;
        private bool needChoice = false;
        public bool CanContinue { get; set; } = true;

        public List<Choice> Choices { get; private set; }
        public string Text { get; private set; }
        public string Speaker { get; private set; }

        private const string SPEAKER_TAG = "speaker";


        private void Awake()
        {
            if (manager != null)
            {
                Debug.LogError("Found more than one Dialogue Manager in the scene");
            }
            manager = this;
        }

        private void OnEnable()
        {
            InputManager.Input.Menu.Submit.canceled += Crutch;
        }

        private void OnDisable()
        {
            InputManager.Input.Menu.Submit.canceled -= Crutch;
        }

        public static DialogueManager GetInstance()
        {
            return manager;
        }

        public void StartDialogue(Dialog dialog)
        {
            currentDialog = dialog;
            TextAsset inkJSON = dialog.DialogINK;
            currentStory = new Story(inkJSON.text);
            OnDialogueStart?.Invoke();
            ContinueDialogue();
        }

        private void EndDialogue()
        {
            OnDialogueEnd?.Invoke();
            currentDialog.OnEnd.Invoke();
        }

        private void ContinueDialogue()
        {
            if (needChoice || !CanContinue)
                return;

            if (currentStory.canContinue)
            {
                Text = currentStory.Continue();
                ParseTags();
                print(manager);
                OnDialogueContinue?.Invoke();
                DisplayChoices();
            }
            else
            {
                EndDialogue();
            }
        }

        private void DisplayChoices()
        {
            Choices = currentStory.currentChoices;
            if (Choices.Count != 0)
            {
                needChoice = true;
                OnDialogueChoices?.Invoke();
            }
        }

        private void ParseTags()
        {
            Speaker = "";
            foreach (string tag in currentStory.currentTags)
            {
                
                string[] splitTag = tag.Split(':');
                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        Speaker = tagValue;
                        break;
                }
            }
            print(manager);
        }

        public void MakeChoice(int index)
        {
            needChoice = false;
            currentStory.ChooseChoiceIndex(index);
            ContinueDialogue();
        }

        private void Crutch(InputAction.CallbackContext a)
        {
            ContinueDialogue();
        }

    }
}

