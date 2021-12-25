using UnityEngine;
using System;
public interface ILabelDrawer
{
     event Action<Vector3, string> LabelDrawEvent;
}