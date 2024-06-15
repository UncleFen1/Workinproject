using System;

namespace Scene
{
    public interface ISceneExecutor
    {
        #region Inits
        void InitScene();
        void GetSettingsAudioScene();
        Action<SettingsScene> OnSetSettingsAudioScene { get; set; }
        void GetSettingsScreenScene();
        Action<SettingsScene> OnSetSettingsScreenScene { get; set; }
        #endregion

        #region Set
        void NewResolution(int indexDrop);
        void NewModeScreen(int _fullScreenMode);
        void AudioNewValueMuz(float value);
        void AudioNewValueEffect(float value);
        void ListDropdownScreen();
        #endregion

        Action OnOpenSceneID { get; set; }
        Action OnLoadScen { get; set; }
        Func<int> OnReBootScen { get; set; }
        Action OnExitGame { get; set; }
        Func<int> OnOpenVictoryScen { get; set; }
        Func<int> OnOpenOverScen { get; set; }
        void OpenScenID(int scenID);
        int GetOpenScenID();
        void LoadScen();
        void ReBootScen();
        void ExitGame();
        void OpenVictoryScen();
        void OpenOverScen();
        void GameTimer(bool isRun);
    }
}