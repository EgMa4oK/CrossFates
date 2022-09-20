using UnityEngine;
using UnityEngine.Playables;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new cutscene", menuName = "Cutscene", order = 50)]
public class Cutscene : ScriptableObject
{
    [SerializeField] private PlayableAsset cutscene;
    [SerializeField] private Condition[] conditions;
    [SerializeField] private UnityEvent onEnd;
    public event Action<PlayableAsset> Play;

    private void OnEnable()
    {
        foreach (Condition condition in conditions)
        {
            condition.onPerform += CanPlay;
        }
    }

    private void CanPlay()
    {
        
        foreach (Condition condition in conditions)
        {
            if (!condition.Performed)
            {
                return;
            }
        }
        Play?.Invoke(cutscene);
    }




}
