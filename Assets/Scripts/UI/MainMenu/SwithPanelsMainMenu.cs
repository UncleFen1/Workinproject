using UnityEngine;
using Zenject;

namespace UI
{
    public class SwithPanelsMainMenu : MonoBehaviour
    {
        [Header("Панель кнопки")]
        [SerializeField] private GameObject buttonPanel;
        [SerializeField] private bool isButtonPanel = true;

        [Header("Панель настроек")]
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private bool isSettingsPanel = true;

        [Header("Панель информации")]
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private bool isInfoPanel = true;

        [Header("Панель результатов")]
        [SerializeField] private GameObject rezultPanel;
        [SerializeField] private bool isRezultPanel = true;

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
            if (isButtonPanel) { panel.OnButtonPanel += ButtonPanel; }
            if (isSettingsPanel) { panel.OnSettingsPanel += SettingsPanel; }
            if (isInfoPanel) { panel.OnInfoPanel += InfoPanel; }
            if (isRezultPanel) { panel.OnRezultPanel += RezultPanel; }
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
                ButtonPanel();
            }
        }
        private void ButtonPanel()
        {
            buttonPanel.SetActive(true);
            settingsPanel.SetActive(false);
            infoPanel.SetActive(false);
            rezultPanel.SetActive(false);
        }
        private void SettingsPanel()
        {
            buttonPanel.SetActive(false);
            settingsPanel.SetActive(true);
            infoPanel.SetActive(false);
            rezultPanel.SetActive(false);
        }
        private void InfoPanel()
        {
            buttonPanel.SetActive(false);
            settingsPanel.SetActive(false);
            infoPanel.SetActive(true);
            rezultPanel.SetActive(false);
        }
        private void RezultPanel()
        {
            buttonPanel.SetActive(false);
            settingsPanel.SetActive(false);
            infoPanel.SetActive(false);
            rezultPanel.SetActive(true);
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

