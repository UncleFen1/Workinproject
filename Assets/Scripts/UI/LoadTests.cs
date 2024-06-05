using DG.Tweening;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LoadTests : MonoBehaviour
    {
        [SerializeField] private bool isRun = false;

        private ISceneExecutor scenes;
        [Inject]
        public void Init(ISceneExecutor _scenes)
        {
            scenes = _scenes;
        }

        private void Update()
        {
            if (isRun) { Debug.Log(scenes.LoadTests()); isRun = false; }
        }

    }
}

