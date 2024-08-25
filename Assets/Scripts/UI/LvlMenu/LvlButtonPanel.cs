using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LvlButtonPanel : MonoBehaviour
    {
        [Header("Кнопка ContinueLevelButton")]
        [SerializeField] private CustomButton continueLevelButton;

        [Header("Кнопка ReBootLevelButton")]
        [SerializeField] private CustomButton reBootLevelButton;

        [Header("Кнопка SettingsButton")]
        [SerializeField] private CustomButton settingsButton;

        [Header("Кнопка InfoButton")]
        [SerializeField] private CustomButton infoButton;

        [Header("Кнопка ExitMainMenuButton")]
        [SerializeField] private CustomButton exitMainMenuButton;

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
            continueLevelButton.onClick.AddListener(() => ContinueLevelButton());

            reBootLevelButton.onClick.AddListener(() => ReBootLevelButton());

            settingsButton.onClick.AddListener(() => Settings());

            infoButton.onClick.AddListener(() => Info());

            exitMainMenuButton.onClick.AddListener(() => ExitMainMenuButton());
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
        private void ContinueLevelButton()
        {
            panel.AudioClick();
            panel.GndPanel();
            scenes.PauseGame(false);
            
        }
        private void ReBootLevelButton()
        {
            panel.AudioClick();
            scenes.ReBootScen();
            scenes.PauseGame(false);
        }
        private void Settings()
        {
            panel.AudioClick();
            panel.SettingsPanel();
        }
        private void Info()
        {
            panel.AudioClick();
            panel.InfoPanel();
        }
        private void ExitMainMenuButton()
        {
            scenes.PauseGame(false);
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

        public void PressContinueLevelButton()
        {
            ContinueLevelButton();
        }
        public bool IsContinueButtonActiveAndEnabled()
        {
            return continueLevelButton.isActiveAndEnabled;
        }
    }
}

