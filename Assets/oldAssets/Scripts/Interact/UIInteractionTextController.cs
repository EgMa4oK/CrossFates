using UnityEngine;
using TMPro;

public class UIInteractionTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private InteractionInstigator watchedInteractionInstigator;

    private void Awake()
    {
        ChangeState();
        watchedInteractionInstigator.InteractableAdded += ChangeState;
        watchedInteractionInstigator.InteractableRemoved += ChangeState;
        watchedInteractionInstigator.OnInteraction += ChangeState;
    }

    private void ChangeState()
    {
        text.enabled = watchedInteractionInstigator.enabled && watchedInteractionInstigator.HasNearbyInteractables();
    }
}
