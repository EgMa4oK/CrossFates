using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static PlayableDirector Timeline { get; private set; }
    [SerializeField] private Cutscene[] cutscenes;

    private void Awake()
    {
        Timeline = GetComponent<PlayableDirector>();
        foreach (Cutscene cutscene in cutscenes)
        {
            cutscene.Play += Play;
        }
    }




    public void Play(PlayableAsset asset)
    {
        Timeline.playableAsset = asset;
        Timeline.Play();
    }
}
