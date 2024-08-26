using System;
using System.IO;
using UnityEngine;

namespace Texts
{
    public enum ModeTxt
    {
        Rus,
        Eng
    }
    public class TextsExecutor : ITexts
    {
        [Header("Имя директории")]
        [SerializeField] private string pathDirectory = "Txt";

        [Header("Имя файла")]
        [SerializeField] private string nameFile = "RusEng";
        private TextCollection[] listData=new TextCollection[0];

        public Action<TextCollection> OnSetText { get { return onSetText; } set { onSetText = value; } }
        private Action<TextCollection> onSetText;

        private TextAsset rusEngTranslationsAsset;
        private TextCollection[] rusEngTranslations;

        public void SetData(TextCollection _textCollection)
        {
            bool isContorlSaveTextCollection = true;
            if (listData.Length == 0) { listData = InitList(); }
            //
            for (int i = 0; i < listData.Length; i++)
            {
                if (listData[i].NameObject == _textCollection.NameObject)
                {
                    isContorlSaveTextCollection = false;
                    onSetText?.Invoke(listData[i]);
                }
            }
            //
            if (isContorlSaveTextCollection)
            {
                _textCollection.RusText = _textCollection.NameObject;
                _textCollection.EngText = _textCollection.NameObject;
                listData = Creat(_textCollection, listData);
                SaveFile(listData);
            }
        }
        private TextCollection[] InitList()
        {
            TextCollection[] templistData;
            templistData = LoadFile();
            return templistData;
        }
        private TextCollection[] LoadFile()
        {
            if (rusEngTranslationsAsset == null) rusEngTranslationsAsset = Resources.Load<TextAsset>("rus-eng");    // without extension .txt or .json
            rusEngTranslations = DeserializeJSON(rusEngTranslationsAsset.text);
            return rusEngTranslations;
        }
        private TextCollection[] DeserializeJSON(string _rezultString)
        {
            TextCollection[] textCollections = JsonConvert.FromJson<TextCollection>(_rezultString);
            return textCollections;
        }
        public TextCollection[] Creat(TextCollection intObject, TextCollection[] massivObject)
        {
            if (massivObject != null)
            {
                int newLength = massivObject.Length + 1;
                Array.Resize(ref massivObject, newLength);
                massivObject[newLength - 1] = intObject;
                return massivObject;
            }
            else
            {
                massivObject = new TextCollection[] { intObject };
                return massivObject;
            }
        }
        //
        private void SaveFile(TextCollection[] _textCollections)
        {
            string _rezultString = ConvertJSON(_textCollections);

            Directory.CreateDirectory(Application.streamingAssetsPath + $"/{pathDirectory}/");
            string pathTxtDoc = Application.streamingAssetsPath + $"/{pathDirectory}/{nameFile}.der1";
            if (File.Exists(pathTxtDoc)) { File.WriteAllText(pathTxtDoc, _rezultString); }
        }
        private string ConvertJSON(TextCollection[] _textCollections)
        {
            string temp = JsonConvert.ToJson(_textCollections, true);
            return temp;
        }
    }
}