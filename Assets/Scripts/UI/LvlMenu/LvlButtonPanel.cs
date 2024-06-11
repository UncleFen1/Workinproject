using DG.Tweening;
using Scene;
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

        [Header("Размеры изменения кнопки")]
        [SerializeField] private float sizeOnButton;

        [Header("Указать ID сцены главного меню")]
        [SerializeField] private int idMainMenuScene = 0;

        [Header("Скорость анимации кнопки")]
        [SerializeField][Range(0.5f, 10f)] private float duration;
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
            continueLevelButton.OnFocusMouse += ButtonSize;
            continueLevelButton.OnPressMouse += ContinueLevelButton;

            reBootLevelButton.OnFocusMouse += ButtonSize;
            reBootLevelButton.OnPressMouse += ReBootLevelButton;

            settingsButton.OnFocusMouse += ButtonSize;
            settingsButton.OnPressMouse += Settings;

            infoButton.OnFocusMouse += ButtonSize;
            infoButton.OnPressMouse += Info;

            exitMainMenuButton.OnFocusMouse += ButtonSize;
            exitMainMenuButton.OnPressMouse += ExitMainMenuButton;
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
        private void ContinueLevelButton(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(panel.GndPanel);
        }
        private void ReBootLevelButton(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(scenes.ReBootScen);
        }
        private void Settings(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(panel.SettingsPanel);
        }
        private void Info(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(panel.InfoPanel);
        }
        private void ExitMainMenuButton(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(()=>scenes.OpenScenID(idMainMenuScene));
        }

        private void ButtonSize(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();

            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

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

