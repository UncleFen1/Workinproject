using DG.Tweening;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class TestVictoryOver : MonoBehaviour
    {
        [Header("Кнопка Victory")]
        [SerializeField] private CustomButton victoryButton;

        [Header("Кнопка Over")]
        [SerializeField] private CustomButton overButton;

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
            victoryButton.OnPressMouse += StartVictory;
            overButton.OnPressMouse += StartOver;
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
        private void StartVictory(bool _flag, GameObject _objectButton)
        {
            panel.AudioClick();
        }
        private void StartOver(bool _flag, GameObject _objectButton)
        {
            panel.AudioClick();
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

