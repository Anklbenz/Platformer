using System.Threading.Tasks;
using Character.States.Data;
using UnityEngine;

namespace Character
{
    public class Flicker
    {
        private const int FREQUENCY = 30;
        private bool _isActive;

        public async void Play(GameObject obj){
            if (!obj) return;
            _isActive = true;

            while (_isActive){
                await Task.Delay(FREQUENCY);
                obj.SetActive(obj.activeSelf == false);

            }

            obj.SetActive(true);
        }

        public void Stop(){
            _isActive = false;
        }
    }
}