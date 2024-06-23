using System;
using Inputs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class TitleRouletteProperties : MonoBehaviour
    {
        [Header("Кнопка LabelRoulElement3Button")]
        [SerializeField] private Text labelRoulElement3Button;

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
            inputs.Enable();
            inputs.OnMousePoint += MousePoint;
        }
        private void MousePoint(InputMouseData _data)
        {
            if (_data.HitObject != null)
            {
                labelRoulElement3Button.text=_data.HitObject.name;
            }
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
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

