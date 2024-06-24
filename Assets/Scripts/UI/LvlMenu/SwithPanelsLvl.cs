using Scene;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SwithPanelsLvl : MonoBehaviour
    {
        [Header("Панель основная")]
        [SerializeField] private GameObject panelGnd;
        [SerializeField] private bool isPanelGnd = true;

        [Header("Панель кнопки")]
        [SerializeField] private GameObject buttonPanel;
        [SerializeField] private bool isButtonPanel = true;

        [Header("Панель настроек")]
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private bool isSettingsPanel = true;

        [Header("Панель информации")]
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private bool isInfoPanel = true;

        private bool isStopClass = false, isRun = false;
        //
        private ISceneExecutor scenes;
        private IMenuExecutor panel;
        [Inject]
        public void Init(ISceneExecutor _scenes, IMenuExecutor _panel)
        {
            panel = _panel;
            scenes = _scenes;
        }
        private void OnEnable()
        {
            if (isPanelGnd) { panel.OnGndPanel += GndPanel; }
            if (isButtonPanel) { panel.OnButtonLvlPanel += ButtonPanel; }
            if (isSettingsPanel) { panel.OnSettingsPanel += SettingsPanel; }
            if (isInfoPanel) { panel.OnInfoPanel += InfoPanel; }
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
                GndPanel();
            }
        }
        private void GndPanel()
        {
            panelGnd.SetActive(true);
            buttonPanel.SetActive(false);
            settingsPanel.SetActive(false);
            infoPanel.SetActive(false);
        }
        private void ButtonPanel()
        {
            panelGnd.SetActive(false);
            buttonPanel.SetActive(true);
            settingsPanel.SetActive(false);
            infoPanel.SetActive(false);
        }
        private void SettingsPanel()
        {
            panelGnd.SetActive(false);
            buttonPanel.SetActive(false);
            settingsPanel.SetActive(true);
            infoPanel.SetActive(false);
        }
        private void InfoPanel()
        {
            panelGnd.SetActive(false);
            buttonPanel.SetActive(false);
            settingsPanel.SetActive(false);
            infoPanel.SetActive(true);
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

