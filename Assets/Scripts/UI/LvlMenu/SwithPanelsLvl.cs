using DG.Tweening;
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

        [Header("Нулевой поинт панелей")]
        [SerializeField] private GameObject startPointPanel;

        [Header("Скорость анимации перемещения панели")]
        [SerializeField][Range(0.5f, 10f)] private float duration;
        private GameObject tempAnimObject;
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
            tempAnimObject = panelGnd;
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
            MovePanel(panelGnd);
        }
        private void ButtonPanel()
        {
            MovePanel(buttonPanel);
        }
        private void SettingsPanel()
        {
            MovePanel(settingsPanel);
        }
        private void InfoPanel()
        {
            MovePanel(infoPanel);
        }
        private void MovePanel(GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(tempAnimObject.transform.DOMove(startPointPanel.transform.position, duration)).SetUpdate(UpdateType.Late, true);;
            tempAnimObject = _objectButton;
            sequence.Append(_objectButton.transform.DOMove(new Vector3(0, 0, 0), duration)).SetUpdate(UpdateType.Late, true);;
            sequence.SetLink(_objectButton);
            
            // sequence.OnComplete(panel.ButtonLvlPanel);
            sequence.OnKill(DoneTween);
            sequence.SetUpdate(true);
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
        private void DoneTween()
        {

        }
    }
}

