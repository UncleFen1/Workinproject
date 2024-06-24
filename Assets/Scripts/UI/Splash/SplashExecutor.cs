using System;
using Scene;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SplashExecutor : MonoBehaviour
    {
        [Header("Кнопка Пропустить")]
        [SerializeField] private CustomButton missButton;

        [Header("Подключить спрайт индикатора нажатия")]
        [SerializeField] private Image indicatorImg;

        [Header("Указать ID загружаемой сцены")]
        [SerializeField] private int idMainMenuScene = 0;

        [Header("Время автозагрузки сцены")]
        [SerializeField][Range(1, 50)] private float clockScene = 10f;

        [Header("Время паузы удержания кнопки")]
        [SerializeField][Range(1, 50)] private int clockButton = 1;

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
            //missButton.onClick.AddListener(()=>ButtonPress(true));
            missButton.OnDown+=ButtonDownPress;
            missButton.OnUp+=ButtonUpPress;
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
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void ButtonDownPress()
        {
            panel.AudioClick();
            isPress =true;
        }
        private void ButtonUpPress()
        {
            panel.AudioClick();
            isPress =false;
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
                countClockButton++;
                if (countClockButton >= clockButton) { countClockButton = 0f; TrigerFillAmount(true); }
            }
            else
            {
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
                fillAmountTik = 0;
            }
            else
            {
                indicatorImg.fillAmount = fillAmountTik;
            }
        }
        private void OnDisable()
        {

        }
    }
}

