using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI
{
    public class SwithPanels : MonoBehaviour
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

        [Header("Нулевой поинт панелей")]
        [SerializeField] private GameObject startPointPanel;

        [Header("Скорость анимации перемещения панели")]
        [SerializeField][Range(0.5f, 10f)] private float duration;
        private GameObject tempAnimObject;
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
            tempAnimObject = buttonPanel;

            if(isButtonPanel){panel.OnButtonPanel += ButtonPanel;}
            if(isSettingsPanel){panel.OnSettingsPanel += SettingsPanel;}
            if(isInfoPanel){panel.OnInfoPanel += InfoPanel;}
            if(isRezultPanel){panel.OnRezultPanel += RezultPanel;}
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
        private void RezultPanel()
        {
            MovePanel(rezultPanel);
        }
        private void MovePanel(GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(tempAnimObject.transform.DOMove(startPointPanel.transform.position, duration));
            tempAnimObject = _objectButton;
            sequence.Append(_objectButton.transform.DOMove(new Vector3(0, 0, 0), duration));
            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
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

