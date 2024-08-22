using System;
using OldSceneNamespace;
using UnityEngine;
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

        [Header("Время паузы удержания кнопки")]
        [SerializeField] private bool isBuildTimeVisible = true;

        private float countClockButton, countClockSec, timeSec;
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

        private void CreateBuildTimeText()
        {
            TextAsset buildTime;
            buildTime = Resources.Load<TextAsset>("app_build_time");    // without .txt
            if (buildTime) 
            {
                Debug.Log($"buildTime: {buildTime.text}");
                CreateText(buildTime.text);
            }
            else Debug.LogWarning("buildTime is null");
        }
        void CreateText(string buildTime)
        {
            if (!isBuildTimeVisible) return;

            var go = new GameObject();
            go.name = "buildTime";
            go.layer = LayerMask.NameToLayer("UI");
            go.transform.parent = FindObjectOfType<Canvas>().transform;
            // go.transform.SetAsFirstSibling();
            go.transform.SetAsLastSibling();
            
            var textAsset = go.AddComponent<Text>();
            textAsset.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); // Default legacy font
            textAsset.fontSize = 32;
            textAsset.alignment = TextAnchor.LowerLeft;
            textAsset.color = new Color(236f/256, 110f/256, 110f/256); // EC6E6E
            textAsset.text = buildTime;

            var rectTransform = go.transform as RectTransform;
            rectTransform.pivot = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero; // Bottom left corner
            rectTransform.anchorMax = Vector2.zero;
            // rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.anchoredPosition = Vector3.zero;
            
            rectTransform.localScale = Vector3.one;
            rectTransform.sizeDelta = new Vector2(1000, 100);
        }

        void Start()
        {
            CreateBuildTimeText();

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
                fillAmountTik += 0.5f;
            }
            else { fillAmountTik -= 0.5f; }

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

