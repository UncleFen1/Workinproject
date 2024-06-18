using UI;
using UnityEngine;
using Zenject;

namespace Scene
{
    public class SettScene : MonoBehaviour
    {
        [Header("Клип музыки")]
        [SerializeField] private AudioClip muzClip;
        [SerializeField] private bool loopMuz = false;

        [Header("Клип эффектов")]
        [SerializeField] private AudioClip effectClip;
        [SerializeField] private bool loopEffect = false;
        private AudioSource muzAudioSource;
        private AudioSource effectAudioSource;
        private bool isStopClass = false, isRun = false;
        //
        private IMenuExecutor panel;
        private ISceneExecutor scenes;
        [Inject]
        public void Init(ISceneExecutor _scenes, IMenuExecutor _panel)
        {
            scenes = _scenes;
            panel = _panel;
        }
        private void OnEnable()
        {
            scenes.OnSetSettingsAudioScene += AudioVolum;
            panel.OnAudioClickl += AudioClick;

            // scenes.OnModeTxt+=ModeTxt;
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                scenes.InitScene();
                if (muzClip != null && effectClip != null) { isRun = true; }
                SettingsAudio();
                scenes.GetSettingsScreenScene();
                scenes.GetModeTxtScene();
            }
        }
        private void SettingsAudio()
        {
            if (muzAudioSource == null) { muzAudioSource = gameObject.AddComponent<AudioSource>(); }
            if (effectAudioSource == null) { effectAudioSource = gameObject.AddComponent<AudioSource>(); }

            muzAudioSource.clip = muzClip;
            muzAudioSource.loop = loopMuz;
            muzAudioSource.Play();

            effectAudioSource.clip = effectClip;
            effectAudioSource.loop = loopEffect;

            scenes.GetSettingsAudioScene();
        }
        private void AudioVolum(SettingsScene _settingsScene)
        {
            muzAudioSource.volume = _settingsScene.MuzValum;
            effectAudioSource.volume = _settingsScene.EffectValum;
        }
        private void AudioClick()
        {
            effectAudioSource.Play();
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

