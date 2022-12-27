using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrossFates.PlayerStates
{

    public class IdleState : PlayerState
    {
        private Controls _controls;
        public IdleState(PlayerCharacter character, Rigidbody2D rigidbody, Controls controls) : base(character, rigidbody)
        {
            _controls = controls;
        }
        public override void Update()
        {
            base.Update();
            Vector2 direction = _controls.Character.Axis.ReadValue<Vector2>();
            if (direction != Vector2.zero)
            {
                _character.SwitchState<RunningState>();
            }
        }
        private void Dash(InputAction.CallbackContext context)
        {
            if ( _character.DashTime + _character.DashCD < Time.time)
            {
                _character.SwitchState<DashState>();
            }
        }
        public override void Enter()
        {
            _controls.Character.Dash.performed += Dash;
        }
        public override void Exit()
        {
            _controls.Character.Dash.performed -= Dash;
        }
    }
}
