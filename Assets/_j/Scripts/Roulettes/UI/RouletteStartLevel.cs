using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace Roulettes
{
    public class RouletteStartLevel : MonoBehaviour
    {
        [SerializeField] private UI.CustomButton followLvlButton;

        protected ISceneExecutor scenes;
        [Inject]
        private void InitBindings(ISceneExecutor _scenes)
        {
            scenes = _scenes;
        }

        private void Start()
        {
            followLvlButton.onClick.AddListener(() => OpenLvl());
        }

        private void OpenLvl()
        {
            // TODO _j don't like it: it's setting ERPROM set flag, so it's increased SceneId+1
            scenes.SetCurrentFlagRoulette(true);
            scenes.OpenScenID(scenes.GetOpenScenID());
        }
    }
}
