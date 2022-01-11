using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Flicker
{
    private const int FREQUENCY = 50;
    private int _flickCount;

    public async void VisibleFlicker(int length, MeshRenderer mesh){
        _flickCount = (int) (length / FREQUENCY);
     
        for (int i = 0; i < _flickCount; i++){
            await Task.Delay(FREQUENCY);
            mesh.enabled = mesh.enabled == false ? true : false;
        }
        mesh.enabled = true;
    }

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

}