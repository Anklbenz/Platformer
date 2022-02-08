using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions
    {
        public event Action<bool> JumpInputEvent, SitInputEvent, ExtraInputEvent;
        public event Action<Vector3> MoveInputEvent;
        public event Action ShootInputEvent;

        private GameInput _gameInput;

        private void OnEnable(){
            if (_gameInput != null) return;

            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Player.SetCallbacks(this);
        }

        private void OnDisable() => _gameInput.Disable();

        public void OnJump(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed)
                JumpInputEvent?.Invoke(true);

            if (context.phase == InputActionPhase.Canceled)
                JumpInputEvent?.Invoke(false);
        }

        public void OnMove(InputAction.CallbackContext context){
            var movement = Vector3.forward * context.ReadValue<float>();
            MoveInputEvent?.Invoke(movement);
        }

        public void OnSitDown(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed)
                SitInputEvent?.Invoke(true);

            if (context.phase == InputActionPhase.Canceled)
                SitInputEvent?.Invoke(false);
        }

        public void OnExtraAction(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed){
                ExtraInputEvent?.Invoke(true);
                ShootInputEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
                ExtraInputEvent?.Invoke(false);
        }
    }
}
