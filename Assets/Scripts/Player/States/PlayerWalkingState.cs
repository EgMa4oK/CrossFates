using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates.PlayerStates
{
    public class WalkingState : MovingState
    {
        public WalkingState(PlayerCharacter character, Rigidbody2D rigidbody, float speed, Controls controls) : base(character, rigidbody, speed, controls)
        {

        }
        public override void Update()
        {
            base.Update();
        }
    }
}