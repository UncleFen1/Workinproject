using Unity.Mathematics;
using UnityEngine;

namespace Inputs
{
    public class MausExecutor
    {
        public MausExecutor() { }
        private Vector3 mousePosition;
        private RaycastHit2D rezultCollider;
        private InputMouseData _inputMouseData;
        private Ray ray;

        public Vector2 MausMoveControl(InputMouseData _inputMouseData, Camera _thisCamera)
        {
            mousePosition = _thisCamera.ScreenToWorldPoint((Vector2)_inputMouseData.MousePosition);
            mousePosition.z = 0f;
            rezultCollider = Physics2D.Raycast(mousePosition, -Vector2.up);

            if (rezultCollider.collider != null)
            {
                Debug.Log(gg.collider.name);
            }

            test(mousePosition, _thisCamera);
            return mousePosition;
        }
        public void test(Vector3 _mousePosition, Camera _thisCamera)
        {
            //ray = _thisCamera.ScreenPointToRay((Vector2)_inputMouseData.MousePosition);
            RaycastHit2D gg = Physics2D.Raycast(_mousePosition, -Vector2.up);
            if (gg.collider != null)
            {
                HitObject
                Debug.Log(gg.collider.name);
            }


        }
    }
}

// ray = _thisCamera.ScreenPointToRay((Vector2)_inputMouseData.MousePosition);//���...�� �����
// if (Physics.Raycast(ray, out hit))
// {
//     return hit;
// }
// return new RaycastHit();

