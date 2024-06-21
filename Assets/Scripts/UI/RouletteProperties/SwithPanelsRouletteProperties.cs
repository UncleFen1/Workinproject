using UnityEngine;
using Zenject;

namespace UI
{
    public class SwithPanelsRouletteProperties : MonoBehaviour
    {
        [Header("Панель GND")]
        [SerializeField] private GameObject gndPanel;

        [Header("Панель RoulProper3Panel")]
        [SerializeField] private GameObject roulProper3Panel;

        [Header("Панель RoulProper4Panel")]
        [SerializeField] private GameObject roulProper4Panel;

        [Header("Панель RoulProper8Panel")]
        [SerializeField] private GameObject roulProper8Panel;

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
            uiGame.OnRoulProper3Panel += RoulProper3Panel;
            uiGame.OnRoulProper4Panel += RoulProper4Panel;
            uiGame.OnRoulProper8Panel += RoulProper8Panel;

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
                roulProper3Panel.SetActive(false);
                roulProper4Panel.SetActive(false);
                roulProper8Panel.SetActive(false);
            }
        }
        private void SetGndPanel()
        {
            roulProper3Panel.SetActive(false);
            roulProper4Panel.SetActive(false);
            roulProper8Panel.SetActive(false);
        }
        private void RoulProper3Panel()
        {
            roulProper3Panel.SetActive(true);
            roulProper4Panel.SetActive(false);
            roulProper8Panel.SetActive(false);
        }
        private void RoulProper4Panel()
        {
            roulProper3Panel.SetActive(false);
            roulProper4Panel.SetActive(true);
            roulProper8Panel.SetActive(false);
        }
        private void RoulProper8Panel()
        {
            roulProper3Panel.SetActive(false);
            roulProper4Panel.SetActive(false);
            roulProper8Panel.SetActive(true);
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

