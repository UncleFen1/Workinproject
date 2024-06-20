using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenuInfoPanel : MonoBehaviour
    {
        [Header("Кнопка ReternSettingsButton")]
        [SerializeField] private CustomButton reternInfoButton;

        private bool isStopClass = false, isRun = false;
        //
        private IMenuExecutor panel;
        [Inject]
        public void Init(IMenuExecutor _panel)
        {
            panel = _panel;
        }

        private void OnEnable()
        {
            reternInfoButton.onClick.AddListener(() => ButtonPanel());
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
            panel.ButtonPanel();
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

