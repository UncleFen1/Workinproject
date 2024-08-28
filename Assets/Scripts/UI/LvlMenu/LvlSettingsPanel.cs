using OldSceneNamespace;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LvlSettingsPanel : MonoBehaviour
    {
        [Header("Dropdown экрана")]
        [SerializeField] private Dropdown screenDropdown;

        [Header("Mode экрана")]
        [SerializeField] private Dropdown modeScreenDropdown;

        [Header("Slider музыки")]
        [SerializeField] private Slider muzSlider;

        [Header("Slider эффектов")]
        [SerializeField] private Slider effectSlider;

        [Header("Кнопка ReternSettingsButton")]
        [SerializeField] private CustomButton reternSettingsButton;

        private bool isStopClass = false, isRun = false;
        //
        private ISceneExecutor scenes;
        private IMenuExecutor panel;
        [Inject]
        public void Init(IMenuExecutor _panel, ISceneExecutor _scenes)
        {
            panel = _panel;
            scenes = _scenes;
        }
        private void OnEnable()
        {
            screenDropdown.onValueChanged.AddListener(NewResolution);

            modeScreenDropdown.onValueChanged.AddListener(NewModeScreen);

            muzSlider.onValueChanged.AddListener(AudioNewValueMuz);
            effectSlider.onValueChanged.AddListener(AudioNewValueEffect);

            scenes.OnSetSettingsAudioScene += SetSettingsAudioScene;
            scenes.OnSetSettingsScreenScene += SetSettingsScreenScene;

            reternSettingsButton.onClick.AddListener(() => ButtonLvlPanel());
        }
        private void SetSettingsAudioScene(SettingsScene _settingsScene)
        {

            muzSlider.value = _settingsScene.MuzValum;
            effectSlider.value = _settingsScene.EffectValum;
        }
        private void SetSettingsScreenScene(SettingsScene _settingsScene)
        {

            screenDropdown.ClearOptions();
            screenDropdown.AddOptions(_settingsScene.ItemsScreen);
            screenDropdown.value = _settingsScene.IdCurrentScreen;

            modeScreenDropdown.ClearOptions();
            modeScreenDropdown.AddOptions(_settingsScene.ScreenModeList);
            modeScreenDropdown.value = _settingsScene.IdCurrentModeScreen;
        }
        private void NewResolution(int _indexDrop)
        {
            scenes.NewResolution(_indexDrop);
            panel.AudioClick();
        }
        private void NewModeScreen(int _indexDrop)
        {
            scenes.NewModeScreen(_indexDrop);
            panel.AudioClick();
        }
        private void AudioNewValueMuz(float _value)
        {
            scenes.AudioNewValueMuz(_value);
        }
        private void AudioNewValueEffect(float _value)
        {
            scenes.AudioNewValueEffect(_value);
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                //scenes.InitScene();
                isRun = true;
            }
        }
        private void ButtonLvlPanel()
        {
            panel.AudioClick();
            panel.ButtonLvlPanel();
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

        public void PressReturnButton()
        {
            ButtonLvlPanel();
        }
        public bool IsReturnButtonActiveAndEnabled()
        {
            return reternSettingsButton.isActiveAndEnabled;
        }
    }
}
