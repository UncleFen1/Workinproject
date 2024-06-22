using System;
using Inputs;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ButtonsPanelsRouletteProperties : MonoBehaviour
    {
        [Header("Кнопка RoulProper3Button")]
        [SerializeField] private CustomButton roulProper3Button;

        [Header("Кнопка RoulProper4Button")]
        [SerializeField] private CustomButton roulProper4Button;

        [Header("Кнопка RoulProper8Button")]
        [SerializeField] private CustomButton roulProper8Button;
        //
        [Header("Кнопка ReternRoulProper3")]
        [SerializeField] private CustomButton reternRoulProper3;

        [Header("Кнопка ReternRoulProper4")]
        [SerializeField] private CustomButton reternRoulProper4;

        [Header("Кнопка ReternRoulProper8")]
        [SerializeField] private CustomButton reternRoulProper8;

        private bool isStopClass = false, isRun = false;
        //
        private IInputPlayerExecutor inputs;
        private IUIGameExecutor uiGame;
        [Inject]
        public void Init(IUIGameExecutor _uiGame, IInputPlayerExecutor _inputs)
        {
            uiGame = _uiGame;
            inputs = _inputs;
        }
        private void OnEnable()
        {
            roulProper3Button.onClick.AddListener(() => RoulProper3());
            roulProper4Button.onClick.AddListener(() => RoulProper4());
            roulProper8Button.onClick.AddListener(() => RoulProper8());

            reternRoulProper3.onClick.AddListener(() => SetGndPanel());
            reternRoulProper4.onClick.AddListener(() => SetGndPanel());
            reternRoulProper8.onClick.AddListener(() => SetGndPanel());
            
            inputs.Enable();
            inputs.OnMousePoint += MousePoint;
        }
        private void MousePoint(Vector2 hit)
        {
            //Debug.Log(hit);
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
        private void SetGndPanel()
        {
            uiGame.SetGndPanel();
        }
        private void RoulProper3()
        {
            uiGame.RoulProper3Button();
        }
        private void RoulProper4()
        {
            uiGame.RoulProper4Button();
        }
        private void RoulProper8()
        {
            uiGame.RoulProper8Button();
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

