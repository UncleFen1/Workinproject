using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Texts
{
    public class LoadTextCollection : MonoBehaviour
    {
        [Header("Имя директории")]
        [SerializeField] private string pathDirectory = "PathDirectory";

        [Header("Имя файла")]
        [SerializeField] private string nameFile = "NameFile";

        [Header("Прочитать")]
        [SerializeField] bool isLoad = false;

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
            if (isLoad)
            {
                isLoad = false;
                LoadFile();
                InitMassiv();
            }
        }
        private void InitMassiv()
        {
            for (int i = 0; i < textCollections.Length; i++)
            {
                Debug.Log(textCollections[i].TextId);
                Debug.Log(textCollections[i].NameObject);
            }
        }
        private void DeserializeJSON(string _rezultString)
        {
            textCollections = JsonConvert.FromJson<TextCollection>(_rezultString);
        }
        private void LoadFile()
        {
            string pathTxtDoc = Application.streamingAssetsPath + $"/{pathDirectory}/{nameFile}.der1";
            string temp = File.ReadAllText(pathTxtDoc);
            DeserializeJSON(temp);
        }
        private void OnDisable()
        {

        }
    }

}
