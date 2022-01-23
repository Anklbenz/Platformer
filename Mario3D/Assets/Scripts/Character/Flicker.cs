using System.Threading.Tasks;
using Character.States.Data;
using UnityEngine;

namespace Character
{
    public class Flicker
    {
        private const int FREQUENCY = 30;
        private bool _isActive;

        public async void FlickerPlay(GameObject obj){
            if (!obj) return;
            _isActive = true;

            while (_isActive){
                await Task.Delay(FREQUENCY);
                obj.SetActive(obj.activeSelf == false);
            }
            obj.SetActive(true);
        }

        public void FlickerStop(){
            _isActive = false;
        }

        /*
        public async void SizeFlicker(int length, StateData current, StateData next){
            _flickCount = (int) (length / FREQUENCY);
            var currentGo = current.SkinGameObject;
            var nextStateGo = next.SkinGameObject;

            for (int i = 0; i < _flickCount; i++){
                await Task.Delay(FREQUENCY);

                if (currentGo.activeSelf){
                    nextStateGo.SetActive(true);
                    currentGo.SetActive(false);
                }
                else{
                    currentGo.SetActive(false);
                    nextStateGo.SetActive(true);
                }
            }
        }
        */

    }

 
}