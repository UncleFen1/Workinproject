using Unity.Mathematics;
using UnityEngine;

namespace Inputs
{
    public class MausExecutor
    {
        public MausExecutor() { }
        private Vector3 mousePosition;
        private RaycastHit hit;

        public Vector2 MausMoveControl(InputMouseData _inputMouseData, Camera _thisCamera)
        {
            mousePosition= _thisCamera.ScreenToWorldPoint((Vector2)_inputMouseData.MousePosition);
            mousePosition.z = 0f;
            return mousePosition;
        }
    }
}


