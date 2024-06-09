using DG.Tweening;
using Scene;
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

        [Header("Размеры изменения кнопки")]
        [SerializeField] private float sizeOnButton;

        [Header("Указать ID загружаемой сцены")]
        [SerializeField] private int idLvlScene = 0;

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
            startLevelButton.OnFocusMouse += ButtonSize;
            startLevelButton.OnPressMouse += StartLevel;

            loadLevelButton.OnFocusMouse += ButtonSize;
            loadLevelButton.OnPressMouse += LoadLevel;

            settingsButton.OnFocusMouse += ButtonSize;
            settingsButton.OnPressMouse += Settings;

            infoButton.OnFocusMouse += ButtonSize;
            infoButton.OnPressMouse += Info;

            rezultButton.OnFocusMouse += ButtonSize;
            rezultButton.OnPressMouse += Rezult;

            exitButton.OnFocusMouse += ButtonSize;
            exitButton.OnPressMouse += Exit;
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
        private void StartLevel(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(() => scenes.OpenScenID(idLvlScene));
        }
        private void LoadLevel(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(scenes.LoadScen);
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
        private void Rezult(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(panel.RezultPanel);

        }
        private void Exit(bool _flag, GameObject _objectButton)
        {
            Sequence sequence = DOTween.Sequence();
            panel.AudioClick();
            if (_flag)
            { sequence.Append(_objectButton.transform.DOScale(sizeOnButton, duration)); }
            else
            { sequence.Append(_objectButton.transform.DOScale(1, duration)); }

            sequence.SetLink(_objectButton);
            sequence.OnKill(DoneTween);
            sequence.OnComplete(scenes.ExitGame);
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

