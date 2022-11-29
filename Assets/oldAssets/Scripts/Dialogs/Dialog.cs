using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[CreateAssetMenu(fileName = "new dialog", menuName = "Dialog/Dialog", order = 50)]
public class Dialog : ScriptableObject
{
    [SerializeField] private bool canRepeat;
    [SerializeField] private TextAsset dialogINK;
    [SerializeField] private List<Condition> conditions;
    [SerializeField] private UnityEvent onEnd;
    public TextAsset DialogINK => dialogINK;
    public UnityEvent OnEnd => onEnd;
    [System.NonSerialized] private int count = 0;

    public void Start()
    {
        DialogueManager manager = DialogueManager.GetInstance();
        manager.StartDialogue(this);
        count++;

    }

    private void OnEnable()
    {
        Crutch.onRestart += Restart;
    }
    private void OnDisable()
    {
        Crutch.onRestart -= Restart;
    }

    private void Restart()
    {
        count = 0;
    }



    public bool CanStart()
    {
        if (!canRepeat && count > 0)
        {
            return false;
        }
        foreach (Condition condition in conditions)
        {
            if (!condition.Performed)
            {
                return false;
            }
        }
        return true;
    }


}
