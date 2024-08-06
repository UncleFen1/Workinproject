using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenuButtonPanel : MonoBehaviour
    {
        [Header("Кнопка StartLevelButton")]
        [SerializeField] private CustomButton startLevelButton;

        [Header("Кнопка LoadLevelButton")]
        [SerializeField] private CustomButton loadLevelButton;

        [Header("Кнопка SettingsButton")]
        [SerializeField] private CustomButton settingsButton;

        [Header("Кнопка InfoButton")]
        [SerializeField] private CustomButton infoButton;

        [Header("Кнопка RezultButton")]
        [SerializeField] private CustomButton rezultButton;

        [Header("Кнопка ExitButton")]
        [SerializeField] private CustomButton exitButton;

        [Header("Указать ID загружаемой сцены")]
        [SerializeField] private int idLvlScene = 0;

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
            startLevelButton.onClick.AddListener(() => StartLevel());

            loadLevelButton.onClick.AddListener(() => LoadLevel());

            settingsButton.onClick.AddListener(() => Settings());

            infoButton.onClick.AddListener(() => Info());

            rezultButton.onClick.AddListener(() => Rezult());

            exitButton.onClick.AddListener(() => Exit());
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
        private void StartLevel()
        {
            panel.AudioClick();
            scenes.OpenScenID(idLvlScene);
        }
        private void LoadLevel()
        {
            panel.AudioClick();
            scenes.LoadScen();
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
        private void Rezult()
        {
            panel.AudioClick();
            panel.RezultPanel();

        }
        private void Exit()
        {
            panel.AudioClick();
            scenes.ExitGame();
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

