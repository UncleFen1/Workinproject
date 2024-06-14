using DG.Tweening;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class VictoryOverPanel : MonoBehaviour
    {
        [Header("Кнопка ReternVictoryOverButton")]
        [SerializeField] private CustomButton reternVictoryOverButton;

        [Header("Размеры изменения кнопки")]
        [SerializeField] private float sizeOnButton;

        [Header("Скорость анимации кнопки")]
        [SerializeField][Range(0.5f, 10f)] private float duration;
        [Header("Указать ID сцены главного меню")]
        [SerializeField] private int idMainMenuScene = 0;
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
            reternVictoryOverButton.OnFocusMouse += ButtonSize;
            reternVictoryOverButton.OnPressMouse += ButtonPanel;
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
            sequence.OnComplete(()=>scenes.OpenScenID(idMainMenuScene));
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

