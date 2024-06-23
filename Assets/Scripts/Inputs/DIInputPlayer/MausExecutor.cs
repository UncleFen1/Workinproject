using UnityEngine;

namespace Inputs
{
    public class MausExecutor
    {
        public MausExecutor() { }
        private Vector3 mousePosition;
        private RaycastHit2D rezultCollider;
        private InputMouseData inputMouseData;

        public InputMouseData MausMoveControl(InputMouseData _inputMouseData, Camera _thisCamera)
        {
            inputMouseData=_inputMouseData;
            mousePosition = _thisCamera.ScreenToWorldPoint(inputMouseData.MousePosition);
            mousePosition.z = 0f;
            inputMouseData.MousePosition=mousePosition;

            rezultCollider = Physics2D.Raycast(mousePosition, -Vector2.up);

            if (rezultCollider.collider != null)
            {
                inputMouseData.HitObject=rezultCollider.collider.gameObject;
            }

            return inputMouseData;
        }
    }
}
