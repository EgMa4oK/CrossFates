using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public override void Activate()
    {
        base.Activate();
        Destroy(gameObject);
    }


}
