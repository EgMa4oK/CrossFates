using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates
{
    public class PickUp : Interactable
    {
        public override void Activate()
        {
            base.Activate();
            Destroy(gameObject);
        }


    }
}
