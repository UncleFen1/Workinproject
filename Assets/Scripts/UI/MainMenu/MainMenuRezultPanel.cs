using DG.Tweening;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenuRezultPanel : MonoBehaviour
    {
        [Header("Кнопка ReternSettingsButton")]
        [SerializeField] private CustomButton reternRezultButton;

        [Header("Размеры изменения кнопки")]
        [SerializeField] private float sizeOnButton;

        [Header("Скорость анимации кнопки")]
        [SerializeField][Range(0.5f, 10f)] private float duration;
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
            reternRezultButton.OnFocusMouse += ButtonSize;
            reternRezultButton.OnPressMouse += ButtonPanel;
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
        private void ButtonPanel(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(panel.ButtonPanel);
        }
        private void ButtonSize(bool _flag, GameObject _objectButton)
        {
            if (_flag)
            { _objectButton.transform.DOScale(sizeOnButton, duration).SetLink(_objectButton).OnKill(DoneTween); }
            else
            { _objectButton.transform.DOScale(1, duration).SetLink(_objectButton).OnKill(DoneTween); }
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

