using System;
using UnityEngine;

namespace Interfaces
{
     public interface ILabelDrawer
     {
          event Action<Vector3, string> LabelDrawEvent;
     }
}