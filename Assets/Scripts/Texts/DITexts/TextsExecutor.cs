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
#if !UNITY_EDITOR && UNITY_WEBGL
// #if UNITY_WEBGL
            // TODO _j DIRTY hacks before rewriting
            string temp = "{\"Items\":[{\"NameObject\":\"MissButtonText\",\"NameScene\":\"SplashScreen0\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"MissButtonText\",\"EngText\":\"MissButtonText\"},{\"NameObject\":\"LoadPanelTitle\",\"NameScene\":\"LoadScene1\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"LoadPanelTitle\",\"EngText\":\"LoadPanelTitle\"},{\"NameObject\":\"IndikatorTxt\",\"NameScene\":\"LoadScene1\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"IndikatorTxt\",\"EngText\":\"IndikatorTxt\"},{\"NameObject\":\"SettingsButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"НАСТРОЙКИ\",\"EngText\":\"SETTINGS\"},{\"NameObject\":\"StartLevelButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"НОВАЯ ИГРА\",\"EngText\":\"NEW GAME\"},{\"NameObject\":\"ExitButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ВЫХОД\",\"EngText\":\"EXIT\"},{\"NameObject\":\"RezultButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"СТАТИСТИКА\",\"EngText\":\"STATISTICS\"},{\"NameObject\":\"PanelGndTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"PanelGndTitle\",\"EngText\":\"PanelGndTitle\"},{\"NameObject\":\"InfoButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"АВТОРЫ\",\"EngText\":\"AUTHORS\"},{\"NameObject\":\"LoadLevelButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ЗАГРУЗИТЬ\",\"EngText\":\"LOAD GAME\"},{\"NameObject\":\"ReternRezultButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"НАЗАД\",\"EngText\":\"RETURN\"},{\"NameObject\":\"RezultPanelTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"СТАТИСТИКА\",\"EngText\":\"STATISTICS\"},{\"NameObject\":\"ReternInfoButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"НАЗАД\",\"EngText\":\"RETURN\"},{\"NameObject\":\"InfoPanelTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"АВТОРЫ\",\"EngText\":\"AUTHORS\"},{\"NameObject\":\"SettingsPanelTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"НАСТРОЙКИ\",\"EngText\":\"SETTINGS\"},{\"NameObject\":\"Label\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"label\",\"EngText\":\"label\"},{\"NameObject\":\"RusToggleLabel\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"РУС\",\"EngText\":\"RUS\"},{\"NameObject\":\"ScreenDropdownTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"РАЗРЕШЕНИЕ ЭКРАНА\",\"EngText\":\"SCREEN RESOLUTION\"},{\"NameObject\":\"EffectSliderTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ГРОМКОСТЬ ЗВУКОВ\",\"EngText\":\"EFFECTS VOLUME\"},{\"NameObject\":\"ReternSettingsButtonText\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"НАЗАД\",\"EngText\":\"RETURN\"},{\"NameObject\":\"MuzSliderTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ГРОМКОСТЬ МУЗЫКИ\",\"EngText\":\"MUSIC VOLUME\"},{\"NameObject\":\"ModeScreenDropdownTitle\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"РЕЖИМ ЭКРАНА\",\"EngText\":\"SCREEN MODE\"},{\"NameObject\":\"EngToggleLabel\",\"NameScene\":\"MainMenu2\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"АНГЛ\",\"EngText\":\"ENG\"},{\"NameObject\":\"VictoryButtonText\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"VictoryButtonText\",\"EngText\":\"VictoryButtonText\"},{\"NameObject\":\"OverButtonText\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"OverButtonText\",\"EngText\":\"OverButtonText\"},{\"NameObject\":\"MenuButtonText\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ПАУЗА\",\"EngText\":\"PAUSE\"},{\"NameObject\":\"ContinueLevelButtonText\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ПРОДОЛЖИТЬ\",\"EngText\":\"CONTINUE\"},{\"NameObject\":\"ExitMainMenuButtonText\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"В ГЛАВНОЕ МЕНЮ\",\"EngText\":\"TO MAIN MENU\"},{\"NameObject\":\"ReBootLevelButtonText\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ЗАНОВО\",\"EngText\":\"RETRY\"},{\"NameObject\":\"ButtonPanelTitle\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ПАУЗА\",\"EngText\":\"PAUSE\"},{\"NameObject\":\"VictoryOverPanelTitle\",\"NameScene\":\"OverScene\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"VictoryOverPanelTitle\",\"EngText\":\"VictoryOverPanelTitle\"},{\"NameObject\":\"FollowLvlButtonText\",\"NameScene\":\"LvL1TrainingScene3\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"FollowLvlButtonText\",\"EngText\":\"FollowLvlButtonText\"},{\"NameObject\":\"LabelRoulElement3Button\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"LabelRoulElement3Button\",\"EngText\":\"LabelRoulElement3Button\"},{\"NameObject\":\"RoulProper8ButtonText\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"RoulProper8ButtonText\",\"EngText\":\"RoulProper8ButtonText\"},{\"NameObject\":\"ReternRoulProper3Text\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ReternRoulProper3Text\",\"EngText\":\"ReternRoulProper3Text\"},{\"NameObject\":\"RoulProper3ButtonText\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"RoulProper3ButtonText\",\"EngText\":\"RoulProper3ButtonText\"},{\"NameObject\":\"RoulProper4ButtonText\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"RoulProper4ButtonText\",\"EngText\":\"RoulProper4ButtonText\"},{\"NameObject\":\"RP3PanelTitle\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"RP3PanelTitle\",\"EngText\":\"RP3PanelTitle\"},{\"NameObject\":\"ReternRoulProper4Text\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ReternRoulProper4Text\",\"EngText\":\"ReternRoulProper4Text\"},{\"NameObject\":\"RP4PanelTitle\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"RP4PanelTitle\",\"EngText\":\"RP4PanelTitle\"},{\"NameObject\":\"LabelRoulElement4Button\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"LabelRoulElement4Button\",\"EngText\":\"LabelRoulElement4Button\"},{\"NameObject\":\"RP8PanelTitle\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"RP8PanelTitle\",\"EngText\":\"RP8PanelTitle\"},{\"NameObject\":\"ReternRoulProper8Text\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"ReternRoulProper8Text\",\"EngText\":\"ReternRoulProper8Text\"},{\"NameObject\":\"LabelRoulElement8Button\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"LabelRoulElement8Button\",\"EngText\":\"LabelRoulElement8Button\"},{\"NameObject\":\"FollowLvl3Text\",\"NameScene\":\"RouletteProperties6\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"FollowLvl3Text\",\"EngText\":\"FollowLvl3Text\"},{\"NameObject\":\"FollowLvl4Text\",\"NameScene\":\"Roulette4Properties7\",\"PoleTxt\":{\"instanceID\":0},\"ModeTxt\":0,\"RusText\":\"FollowLvl4Text\",\"EngText\":\"FollowLvl4Text\"},{\"NameObject\":\"FollowLvl8Text\",\"NameScene\":\"Roulette8Properties8\",\"PoleTxt\":{\"instanceID\":52586},\"ModeTxt\":0,\"RusText\":\"FollowLvl8Text\",\"EngText\":\"FollowLvl8Text\"}]}";
            return DeserializeJSON(temp);
#else
            string pathTxtDoc = Application.streamingAssetsPath + $"/{pathDirectory}/{nameFile}.der1";
            string temp = File.ReadAllText(pathTxtDoc);
            return DeserializeJSON(temp);
#endif
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