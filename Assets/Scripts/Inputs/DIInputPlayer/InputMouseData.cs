using Unity.Mathematics;
using UnityEngine;

namespace Inputs
{
    public struct InputMouseData
    {
        public Vector2 MouseAxes;//мыш оси
        public Vector2 MousePosition;//мыш позиция
        public float MouseLeftButton;//мыш левая
        public float MouseMiddleButton;//мыш колесо
        public float MouseRightButton;//мыш правая
        public GameObject HitObject;
    }
}

