using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
   public class Factory : ScriptableObject
   {
      protected T Get<T>(T prefab, Vector3 pos, Transform parent) where T : MonoBehaviour{
         return Instantiate(prefab, pos, Quaternion.identity, parent);
      }
   }
}