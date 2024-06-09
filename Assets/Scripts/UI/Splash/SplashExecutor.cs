using DG.Tweening;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SplashExecutor : MonoBehaviour
    {
        [Header("Кнопка Пропустить")]
        [SerializeField] private CustomButton missButton;

        [Header("Размеры изменения кнопки")]
        [SerializeField] private float sizeOnButton;

        [Header("Подключить спрайт индикатора нажатия")]
        [SerializeField] private Image indicatorImg;

        [Header("Указать ID загружаемой сцены")]
        [SerializeField] private int idMainMenuScene = 0;

        [Header("Время автозагрузки сцены")]
        [SerializeField][Range(1, 50)] private float clockScene = 10f;

        [Header("Время паузы удержания кнопки")]
        [SerializeField][Range(1, 50)] private int clockButton = 1;

        [Header("Скорость анимации кнопки")]
        [SerializeField][Range(0.5f, 10f)] private float duration;
        private float countClockButton, timeClick, countClockSec, timeSec;
        private float fillAmountTik = 0;
        private bool isPress;
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
            missButton.OnFocusMouse += ButtonSize;
            missButton.OnPressMouse += ButtonPress;
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
        private void ButtonPress(bool _flag, GameObject _objectButton)
        {
            panel.AudioClick();
            if (_flag)
            {
                isPress = _flag;
                ButtonSize(false, _objectButton);
            }
            else
            {
                isPress = _flag;
                ButtonSize(true, _objectButton);
            }
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

            // if (_flag)
            // { _objectButton.transform.DOScale(sizeOnButton, duration).SetLink(_objectButton).OnKill(DoneTween); }
            // else
            // { _objectButton.transform.DOScale(1, duration).SetLink(_objectButton).OnKill(DoneTween); }
        }

        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            ClockLoadScene();
            ClockPressButton();
        }
        private void ClockLoadScene()
        {
            if (timeSec + 1 <= Time.time)
            {
                timeSec = Time.time;
                countClockSec++;
                if (countClockSec >= clockScene) { countClockSec = 0f; scenes.OpenScenID(idMainMenuScene); }
            }
        }
        private void ClockPressButton()
        {
            if (isPress)
            {
                if (timeClick <= Time.time)
                {
                    timeClick = Time.time;
                    countClockButton++;
                    if (countClockButton >= clockButton) { countClockButton = 0f; TrigerFillAmount(true); }
                }
            }
            else
            {
                timeClick = Time.time;
                countClockButton++;
                if (countClockButton >= clockButton) { countClockButton = 0f; TrigerFillAmount(false); }
            }
        }
        private void TrigerFillAmount(bool isSummCount)
        {
            if (isSummCount)
            {
                fillAmountTik += 0.1f;
            }
            else { fillAmountTik -= 0.1f; }

            if (fillAmountTik >= 1)
            {
                indicatorImg.fillAmount = 1;
                scenes.OpenScenID(idMainMenuScene);
            }
            else
            {
                indicatorImg.fillAmount = fillAmountTik;
            }

            if (fillAmountTik <= 0)
            {
                indicatorImg.fillAmount = 0;
            }
            else
            {
                indicatorImg.fillAmount = fillAmountTik;
            }
        }
        private void OnDisable()
        {

        }
        private void DoneTween()
        {

        }
    }
}

