using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Texts
{
    public class SaveTextCollection : MonoBehaviour
    {
        [Header("Имя директории")]
        [SerializeField] private string pathDirectory = "PathDirectory";

        [Header("Имя файла")]
        [SerializeField] private string nameFile = "NameFile";

        [Header("Записать")]
        [SerializeField] bool isSave = false;

        [Header("Указать ссылку на текстовое поле с указанием имени")]
        [SerializeField] private Text[] gameObjectsText;

        [Header("Содержимое согласно id")]
        [SerializeField] private string[] rusTexts;
        [SerializeField] private string[] engTexts;
        private TextCollection[] textCollections;
        private bool isStopClass = false, isRun = false;
        private void OnEnable()
        {

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
            if (isSave)
            {
                isSave = false;
                InitMassiv();
                ConvertJSON();
            }
        }
        private void InitMassiv()
        {
            textCollections = new TextCollection[gameObjectsText.Length];
            for (int i = 0; i < textCollections.Length; i++)
            {
                textCollections[i].NameObject = $"{gameObjectsText[i].name}";

                if (rusTexts[i] == null) { textCollections[i].RusText = "-"; }
                else { textCollections[i].RusText = rusTexts[i]; }

                if (engTexts[i] == null) { textCollections[i].EngText = "-"; }
                else { textCollections[i].EngText = engTexts[i]; }
            }
        }
        private void ConvertJSON()
        {
            string temp = JsonConvert.ToJson(textCollections, true);
            SaveFile(temp);
        }
        private void SaveFile(string _rezultString)
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + $"/{pathDirectory}/");
            string pathTxtDoc = Application.streamingAssetsPath + $"/{pathDirectory}/{nameFile}.der1";
            if (File.Exists(pathTxtDoc))
            {
                File.WriteAllText(pathTxtDoc, _rezultString);
            }

        }
        private void OnDisable()
        {

        }
    }

}
