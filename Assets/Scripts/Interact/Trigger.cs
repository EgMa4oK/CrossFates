using UnityEngine;


namespace CrossFates
{
    [RequireComponent(typeof(Collider2D))]
    public class Trigger : Activator
    {

        private void OnTriggerEnter2D()
        {
            Activate();
        }
    }
}
