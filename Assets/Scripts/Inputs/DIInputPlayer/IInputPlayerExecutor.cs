using System;
using UnityEngine;

namespace Inputs
{
    public interface IInputPlayerExecutor
    {
        Action<InputMouseData> OnMoveMouse { get; set; }
        Camera Camera { get; set; }
        Action<InputMouseData> OnMousePoint { get; set; }
        Action<InputMouseData> OnStartPressMouse { get; set; }
        Action<InputButtonData> OnMoveButton { get; set; }
        Action<InputButtonData> OnStartPressButton { get; set; }
        Action<InputButtonData> OnCancelPressButton { get; set; }
        void Enable();
        void OnDisable();
    }
}