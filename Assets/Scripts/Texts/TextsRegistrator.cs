using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Texts
{
    public class TextsRegistrator : MonoBehaviour
    {
        public string thisName;
        private bool isStopClass = false, isRun = false;

        private ITexts texts;
        [Inject]
        public void Init(ITexts _texts)
        {
            texts = _texts;
        }
        private void OnEnable()
        {

        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            thisName = gameObject.name;
            TextCollection element = new TextCollection
            {
                NameObject = thisName,
                PoleTxt = GetComponent<Text>(),
                RusText = "null",
                EngText = "null"
            };
            texts.SetData(element);
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

