using UnityEngine;


namespace CrossFates
{
    [RequireComponent(typeof(Collider2D))]
    public class Trigger : Activator
    {

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.GetComponent<PlayerCharacter>() != null)
            {
                Activate();
            }          
        }
    }
}
