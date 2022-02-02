using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu]
    public class InputReader : ScriptableObject, GameInput.IPlayerActions
    {
        public event Action<bool> JumpEvent, SitDownEvent, ExtraActionEvent;
        public event Action<Vector3> MoveEvent;
        public event Action ShootEvent;

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
                JumpEvent?.Invoke(true);

            if (context.phase == InputActionPhase.Canceled)
                JumpEvent?.Invoke(false);
        }

        public void OnMove(InputAction.CallbackContext context){
            var movement = Vector3.forward * context.ReadValue<float>();
            MoveEvent?.Invoke(movement);
        }

        public void OnSitDown(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed)
                SitDownEvent?.Invoke(true);

            if (context.phase == InputActionPhase.Canceled)
                SitDownEvent?.Invoke(false);
        }

        public void OnExtraAction(InputAction.CallbackContext context){
            if (context.phase == InputActionPhase.Performed){
                ExtraActionEvent?.Invoke(true);
                ShootEvent?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
                ExtraActionEvent?.Invoke(false);
        }
    }
}
