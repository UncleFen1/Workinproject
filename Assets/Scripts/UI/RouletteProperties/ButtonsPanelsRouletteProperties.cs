using System;
using Inputs;
using Scene;
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

        [Header("Указать ID загружаемой сцены")]
        [SerializeField] protected int idLvlScene = 0;

        private bool isStopClass = false, isRun = false;
        //
        private IInputPlayerExecutor inputs;
        private IUIGameExecutor uiGame;
        private ISceneExecutor scenes;
        [Inject]
        public void Init(IUIGameExecutor _uiGame, IInputPlayerExecutor _inputs, ISceneExecutor _scenes)
        {
            uiGame = _uiGame;
            inputs = _inputs;
            scenes = _scenes;
        }
        private void OnEnable()
        {
            // roulProper3Button.onClick.AddListener(() => RoulProper3());
            // roulProper4Button.onClick.AddListener(() => RoulProper4());
            // roulProper8Button.onClick.AddListener(() => RoulProper8());

            reternRoulProper3.onClick.AddListener(() => OpenLvl());
            reternRoulProper4.onClick.AddListener(() => OpenLvl());
            reternRoulProper8.onClick.AddListener(() => OpenLvl());
            // inputs.Enable();
            // inputs.OnMousePoint += MousePoint;
            // inputs.OnStartPressMouse += MousePress;
        }
        // private void MousePoint(InputMouseData _data)
        // {
        //     if (_data.HitObject != null)
        //     {
        //         tempGameObject=_data.HitObject;
        //         Debug.Log(_data.HitObject.name);
        //     }
        //     else{tempGameObject=null;}
        // }
        // private void MousePress(InputMouseData _data)
        // {
        //      if (tempGameObject != null & _data.MouseLeftButton!=0)
        //     {
        //         Debug.Log($"{tempGameObject.name} + {_data.MouseLeftButton} = нажата кнопка");
        //     }

        // }
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
        private void OpenLvl()
        {
            scenes.OpenScenID(idLvlScene);
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

