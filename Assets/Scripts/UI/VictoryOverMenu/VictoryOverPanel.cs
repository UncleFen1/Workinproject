using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace UI
{
    public class VictoryOverPanel : MonoBehaviour
    {
        [Header("Кнопка ReternVictoryOverButton")]
        [SerializeField] private CustomButton reternVictoryOverButton;

        [Header("Указать ID сцены главного меню")]
        [SerializeField] private int idMainMenuScene = 0;
        private bool isStopClass = false, isRun = false;
        //
        private ISceneExecutor scenes;
        private IMenuExecutor panel;
        [Inject]
        public void Init(ISceneExecutor _scenes, IMenuExecutor _panel)
        {
            scenes = _scenes;
            panel = _panel;
        }

        private void OnEnable()
        {
            reternVictoryOverButton.onClick.AddListener(ButtonPanel);
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                isRun = true;
            }
        }
        private void ButtonPanel()
        {
            panel.AudioClick();
            scenes.OpenScenID(idMainMenuScene);
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

