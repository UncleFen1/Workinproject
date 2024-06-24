using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Texts
{
    public class TextsRegistrator : MonoBehaviour
    {
        public string thisName;
        private TextCollection element;
        private Text poleTxt;

        private bool isStopClass = false, isRun = false;

        private ITexts texts;
        private ISceneExecutor scenes;
        [Inject]
        public void Init(ITexts _texts, ISceneExecutor _scenes)
        {
            texts = _texts;
            scenes = _scenes;
        }
        private void OnEnable()
        {
            poleTxt = GetComponent<Text>();
            texts.OnSetText += SetText;
            scenes.OnModeTxt += ModeText;
        }
        private void ModeText(SettingsScene _settingsScene)
        {
            if (_settingsScene.ModeText == ModeTxt.Rus) { poleTxt.text = element.RusText; }
            if (_settingsScene.ModeText == ModeTxt.Eng) { poleTxt.text = element.EngText; }
        }
        private void SetText(TextCollection _textCollection)
        {
            if (_textCollection.NameObject == element.NameObject)
            {
                element = _textCollection;
            }
            else { return; }
            //
            scenes.GetModeTxtScene();
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            thisName = gameObject.name;
            element = new TextCollection
            {
                NameObject = thisName,
                NameScene = SceneManager.GetActiveScene().name,
                PoleTxt = poleTxt,
            };
            texts.SetData(element);
            //
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

