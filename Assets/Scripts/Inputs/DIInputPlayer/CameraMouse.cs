using UnityEngine;
using Zenject;

namespace Inputs
{
    public class CameraMouse : MonoBehaviour
    {
        private Camera _camera;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs)
        {
            inputs = _inputs;
        }
        private void Start()
        {
            _camera=GetComponent<Camera>();
            inputs.Camera = _camera;
        }
    }
}
