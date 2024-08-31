using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LvlGndPanel : MonoBehaviour
    {
        [Header("Кнопка MenuButton")]
        [SerializeField] private CustomButton menuButton;

        private bool isStopClass = false, isRun = false;
        //
        private IMenuExecutor panel;
        private ISceneExecutor scenes;
        [Inject]
        public void Init(IMenuExecutor _panel,ISceneExecutor _scenes)
        {
            panel = _panel;
            scenes = _scenes;
        }
        private void OnEnable()
        {
            menuButton.onClick.AddListener(() => ButtonLvlPanel());
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
        private void ButtonLvlPanel()
        {
            menuButton.GetComponentInParent<Canvas>().sortingOrder = 100;
            panel.AudioClick();
            panel.ButtonLvlPanel();
            scenes.PauseGame(true);
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

        public void PressPauseButton()
        {
            ButtonLvlPanel();
        }
        public bool IsButtonActiveAndEnabled()
        {
            return menuButton.isActiveAndEnabled;
        }
        public CustomButton GetMenuButton()
        {
            return menuButton;
        }
    }
}

