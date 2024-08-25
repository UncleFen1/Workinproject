using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace UI
{
    public class TestVictoryOver : MonoBehaviour
    {
        [Header("Кнопка FollowLvl")]
        [SerializeField] private CustomButton followLvlButton;
        [SerializeField] private int followSceneId=0;

        [Header("Кнопка Victory")]
        [SerializeField] private CustomButton victoryButton;

        [Header("Кнопка Over")]
        [SerializeField] private CustomButton overButton;
        [Header("Кнопка CheatsOn")]
        [SerializeField] private CustomButton cheatsButton;

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
            followLvlButton.onClick.AddListener(() => FollowLvl());
            victoryButton.onClick.AddListener(() => StartVictory());
            overButton.onClick.AddListener(() => StartOver());
            cheatsButton.onClick.AddListener(() => CheatsOn());
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
        private void FollowLvl()
        {
            panel.AudioClick();
            scenes.OpenScenID(followSceneId);
        }
        private void StartVictory()
        {
            panel.AudioClick();
            scenes.OpenVictoryScen();
        }
        private void StartOver()
        {
            panel.AudioClick();
            scenes.OpenOverScen();
        }
        private void CheatsOn()
        {
            Debug.LogWarning("_j CHEATS ON");
            panel.AudioClick();
            var playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.maxPlayerHealth = 9999;
                playerHealth.currentPlayerHealth = 9999;
            }
            // TryGetComponent
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

