using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using TMPro;
using static UnityEngine.InputSystem.InputAction;
using System.Threading.Tasks;

namespace CrossFates
{
    public class DialogueUI : MonoBehaviour
    {
        private DialogueManager manager;

        [SerializeField] private GameObject dialoguePannel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private List<GameObject> choices;
        [SerializeField] private GameObject choicePanel;
        private List<TextMeshProUGUI> choicesText;
        [SerializeField] TextMeshProUGUI speakerName;

        [SerializeField] private float typingTime = 0.1f;
        private bool isTyping = false;


        private void Start()
        {
            manager = DialogueManager.GetInstance();

            dialoguePannel.SetActive(false);
            choicePanel.SetActive(false);

            choicesText = new List<TextMeshProUGUI>();
            foreach (GameObject choice in choices)
            {
                choicesText.Add(choice.GetComponentInChildren<TextMeshProUGUI>());
            }
        }

        private void OnEnable()
        {
            
            DialogueManager.OnDialogueStart += ShowDialogue;
            DialogueManager.OnDialogueEnd += HideDialogue;
            DialogueManager.OnDialogueContinue += ContinueDialgoue;
            DialogueManager.OnDialogueChoices += DisplayChoices;
            InputManager.Input.Menu.Submit.canceled += Crutch;
        }

        private void OnDisable()
        {
            
            DialogueManager.OnDialogueStart -= ShowDialogue;
            DialogueManager.OnDialogueEnd -= HideDialogue;
            DialogueManager.OnDialogueContinue -= ContinueDialgoue;
            DialogueManager.OnDialogueChoices -= DisplayChoices;
            InputManager.Input.Menu.Submit.canceled -= Crutch;
        }

        private void ShowDialogue()
        {    
            dialoguePannel.SetActive(true);
        }

        private void HideDialogue()
        {
            dialoguePannel.SetActive(false);
        }

        private void ContinueDialgoue()
        {
            speakerName.text = manager.Speaker;
            DisplayLine(manager.Text);
        }

        private async void DisplayLine(string line)
        {
            manager.CanContinue = false;
            isTyping = true;
            dialogueText.text = "";
            foreach (char letter in line)
            {
                if (isTyping)
                {
                    dialogueText.text += letter; 
                    await Task.Delay((int)(typingTime * 1000));
                } 
            }
            manager.CanContinue = true;
            isTyping = false;
        }

        private void SkipTyping()
        {
            if (isTyping)
            {
                dialogueText.text = manager.Text;
                isTyping = false;
            }
        }

        private void DisplayChoices()
        {

            List<Choice> currentChoices = manager.Choices;

            choicePanel.SetActive(true);

            if (currentChoices.Count > choices.Count)
            {
                GameObject button = Instantiate(choices[0], choices[0].transform.parent);
                choices.Add(button);
                choicesText.Add(button.GetComponentInChildren<TextMeshProUGUI>());
            }

            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                index++;
            }

            for (int i = index; i < choices.Count; i++)
            {
                choices[i].SetActive(false);
            }
        }

        public void MakeChoice(GameObject self)
        {
            EventSystem.current.SetSelectedGameObject(null);

            int choiceIndex = choices.IndexOf(self);
            choicePanel.SetActive(false);
            manager.MakeChoice(choiceIndex);
        }

        private void Crutch(CallbackContext a)
        {

            SkipTyping();


        }
    }
}
