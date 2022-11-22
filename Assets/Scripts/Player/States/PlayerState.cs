using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossFates.PlayerStates
{
    public abstract class PlayerState
    {
        protected Rigidbody2D _rigidbody;
        protected PlayerCharacter _character;
        public PlayerState(PlayerCharacter character, Rigidbody2D rigidbody) 
        {
            _character = character;
            _rigidbody = rigidbody;
        }
        public virtual void Update() { }
        public virtual void PhysicUpdate() { }
        public virtual void Enter() { }
        public virtual void Exit() { }
    }

}