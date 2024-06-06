using Scene;
using UnityEngine;
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
        private void OnEnable()
        {
            scenes.InitScene();
            scenes.OnSetSettingsScene += AudioVolum;
        }
        private void AudioVolum(SettingsScene _settingsScene)
        {
            Debug.Log($"{_settingsScene.isLoad}=={_settingsScene.MuzValum}{_settingsScene.EffectValum}");
        }
        private void Update()
        {
            if (isRun) { scenes.GetSettingsScene(); isRun = false; }
        }

    }
}

