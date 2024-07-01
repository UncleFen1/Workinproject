using UnityEngine;
using Zenject;

namespace UI
{
    public class SwithPanelsRouletteProperties : MonoBehaviour
    {
        [Header("Панель GND")]
        [SerializeField] private GameObject gndPanel;

        [Header("Панель RoulProperPanel")]
        [SerializeField] private GameObject roulProperPanel;

        private bool isStopClass = false, isRun = false;
        //
        private IUIGameExecutor uiGame;
        [Inject]
        public void Init(IUIGameExecutor _uiGame)
        {
            uiGame = _uiGame;
        }
        private void OnEnable()
        {
            uiGame.OnRoulProper3Panel += RoulProperPanel;

            uiGame.OnSetGndPanel += SetGndPanel;
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
                gndPanel.SetActive(false);
                roulProperPanel.SetActive(true);
            }
        }
        private void SetGndPanel()
        {
            gndPanel.SetActive(true);
            roulProperPanel.SetActive(false);
        }
        private void RoulProperPanel()
        {
            gndPanel.SetActive(false);
            roulProperPanel.SetActive(true);
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

