using System;
using System.Collections.Generic;
using System.Linq;
using Texts;
using UnityEngine;

namespace OldSceneNamespace
{
    public struct SettingsScene
    {
        public int isLoad;//true=1 false=0
        public float MuzValum;
        public float EffectValum;
        //
        public ModeTxt ModeText;
        //
        public List<string> ItemsScreen;
        public int Width;
        public int Height;
        public double CurrentHz;
        // public FullScreenMode ScreenMode;
        public List<string> ScreenModeList;
        public int IdCurrentScreen;
        public int IdCurrentModeScreen;
    }

    public class SceneExecutor : ISceneExecutor
    {
        private SettingsScene settingsScene;
        private List<string> tempScreenList, tempModeScreenList;
        //
        public Action<SettingsScene> OnSetSettingsAudioScene { get { return onSetSettingsAudioScene; } set { onSetSettingsAudioScene = value; } }
        private Action<SettingsScene> onSetSettingsAudioScene;
        public Action<SettingsScene> OnSetSettingsScreenScene { get { return onSetSettingsScreenScene; } set { onSetSettingsScreenScene = value; } }
        private Action<SettingsScene> onSetSettingsScreenScene;
        public Action OnOpenSceneID { get { return onOpenSceneID; } set { onOpenSceneID = value; } }
        private Action onOpenSceneID;
        public Action OnLoadScen { get { return onLoadScen; } set { onLoadScen = value; } }
        private Action onLoadScen;
        public Func<int> OnReBootScen { get { return onReBootScen; } set { onReBootScen = value; } }
        private Func<int> onReBootScen;
        public Action OnExitGame { get { return onExitGame; } set { onExitGame = value; } }
        private Action onExitGame;
        public Action<SettingsScene> OnModeTxt { get { return onModeTxt; } set { onModeTxt = value; } }
        private Action<SettingsScene> onModeTxt;
        public Func<int> OnOpenVictoryScen { get { return onOpenVictoryScen; } set { onOpenVictoryScen = value; } }
        private Func<int> onOpenVictoryScen;
        public Func<int> OnOpenOverScen { get { return onOpenOverScen; } set { onOpenOverScen = value; } }
        private Func<int> onOpenOverScen;
        public Action<bool> OnPauseGame { get { return onPauseGame; } set { onPauseGame = value; } }
        private Action<bool> onPauseGame;

        #region SwitchScene
        public void OpenScenID(int _scenID)
        {
            SetIDScene(_scenID);
            onOpenSceneID?.Invoke();
        }
        public int GetOpenScenID()
        {
            return GetIDScene();
        }
        public void SetCurrentFlagRoulette(bool _isFlag)
        {
            SetFlagRoulette(_isFlag);
        }
        public bool GetCurrentFlagRoulette()
        {
            return GetFlagRoulette();
        }
        public void LoadScen()//меню не сделал
        {
            onLoadScen?.Invoke();
        }
        public void ReBootScen()
        {
            int currentScene = (int)onReBootScen?.Invoke();
            SetIDScene(currentScene);
        }
        public void ExitGame()
        {
            SetIDScene(0);//запишм 0 по выходу
            onExitGame?.Invoke();
        }
        public void OpenVictoryScen()
        {
            int victoryScene = (int)onOpenVictoryScen?.Invoke();
            SetIDScene(victoryScene);
        }
        public void OpenOverScen()
        {
            int overScene = (int)onOpenOverScen?.Invoke();
            SetIDScene(overScene);
        }
        public void PauseGame(bool isRun)
        {
            if (!isRun) { Time.timeScale = 1; }
            else { Time.timeScale = 0; }
        }
        #endregion

        #region SettingsScene
        public void InitScene()
        {
            NewSettings();

            Resolution currentResolution = Screen.currentResolution;
            settingsScene.CurrentHz = currentResolution.refreshRateRatio.value;
            ListDropdownScreen();
            ListModeDropdownScreen();
            if (settingsScene.isLoad == 0)
            {
                settingsScene.EffectValum = 0.5f;
                settingsScene.MuzValum = 0.5f;
                SetAudioParametr(settingsScene);

                settingsScene.Height = currentResolution.height;
                settingsScene.Width = currentResolution.width;
                settingsScene.IdCurrentModeScreen = 1;
                SetResolution(settingsScene);

                settingsScene.isLoad = 1;
                SetIsLoad(settingsScene);

                settingsScene.ModeText = ModeTxt.Rus;
                SetLangScene(settingsScene.ModeText);

            }
            
            Debug.Log("_j InitScene, there was setting resolution, but I see no reason for that");
            // Debug.Log($"{settingsScene.Width}x{settingsScene.Height}");
            // Screen.SetResolution(settingsScene.Width, settingsScene.Height, (FullScreenMode)settingsScene.IdCurrentModeScreen);
        }
        private void NewSettings()
        {
            settingsScene = new SettingsScene();
            GetIsLoad();
            GetResolution();
            GetAudioParametr();
            GetLangScene();
        }
        public void NewLangText(ModeTxt _modeTxt)
        {
            settingsScene.ModeText = _modeTxt;
            SetLangScene(settingsScene.ModeText);
            GetModeTxtScene();
        }
        public void GetModeTxtScene()
        {
            onModeTxt?.Invoke(settingsScene);
        }
        public void GetSettingsAudioScene()
        {
            onSetSettingsAudioScene?.Invoke(settingsScene);
        }
        public void GetSettingsScreenScene()
        {
            onSetSettingsScreenScene?.Invoke(settingsScene);
        }
        public void NewResolution(int _indexDrop)
        {
            string newTextScreen = tempScreenList.ElementAt(_indexDrop);
            Resolution[] resolutions = Screen.resolutions;
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (settingsScene.CurrentHz == resolutions[i].refreshRateRatio.value)
                {
                    if ($"{resolutions[i].width}x{resolutions[i].height} -{resolutions[i].refreshRateRatio.value}" == newTextScreen)
                    {
                        settingsScene.Width = resolutions[i].width;
                        settingsScene.Height = resolutions[i].height;
                        settingsScene.IdCurrentScreen = i;
                        
                        Debug.Log("_j NewResolution, there was setting resolution, but I see no reason for that");
                        // SetResolution(settingsScene);
                        // Screen.SetResolution(settingsScene.Width, settingsScene.Height, (FullScreenMode)settingsScene.IdCurrentModeScreen);
                        
                        //GetSettingsScreenScene();
                    }
                }
            }
        }
        public void NewModeScreen(int _fullScreenMode)
        {
            settingsScene.IdCurrentModeScreen = _fullScreenMode;
            SetResolution(settingsScene);
            
            Debug.Log("_j NewModeScreen, there was setting resolution, but I see no reason for that");
            // Screen.SetResolution(settingsScene.Width, settingsScene.Height, (FullScreenMode)settingsScene.IdCurrentModeScreen);
            
            //GetSettingsScreenScene();
        }
        public void ListDropdownScreen()
        {
            tempScreenList = new List<string>();
            Resolution[] resolutions = Screen.resolutions;

            for (int i = 0; i < resolutions.Length; i++)
            {
                if (settingsScene.CurrentHz == resolutions[i].refreshRateRatio.value)
                {
                    tempScreenList.Add($"{resolutions[i].width}x{resolutions[i].height} -{resolutions[i].refreshRateRatio.value}");
                    if (settingsScene.Height == resolutions[i].height &&
                         settingsScene.Width == resolutions[i].width)
                    {
                        settingsScene.IdCurrentScreen = i;
                    }
                }
            }
            settingsScene.ItemsScreen = tempScreenList;
        }
        public void ListModeDropdownScreen()
        {
            tempModeScreenList = new List<string>();

            foreach (string name in Enum.GetNames(typeof(FullScreenMode)))
            {
                tempModeScreenList.Add(name);
            }
            settingsScene.ScreenModeList = tempModeScreenList;
        }
        public void AudioNewValueMuz(float value)
        {
            if (settingsScene.isLoad == 1)
            {
                settingsScene.MuzValum = value;
                SetAudioParametr(settingsScene);
                GetSettingsAudioScene();
            }
        }
        public void AudioNewValueEffect(float value)
        {
            if (settingsScene.isLoad == 1)
            {
                settingsScene.EffectValum = value;
                SetAudioParametr(settingsScene);
                GetSettingsAudioScene();
            }
        }
        #endregion

        #region EPROM
        private void SetFlagRoulette(bool _isFlag)
        {
            if(_isFlag){PlayerPrefs.SetInt("EPROMFlagRoulette", 1);}
            else{PlayerPrefs.SetInt("EPROMFlagRoulette", 0);}
        }
        private bool GetFlagRoulette()
        {
            if(PlayerPrefs.GetInt("EPROMFlagRoulette")==1){return true;}
            return false;
        }
        private void SetLangScene(ModeTxt _modeTxt)
        {
            PlayerPrefs.SetInt("EPROMLangScene", (int)_modeTxt);
        }
        private void GetLangScene()
        {
            settingsScene.ModeText = (ModeTxt)PlayerPrefs.GetInt("EPROMLangScene");
        }
        private void SetIDScene(int _idScene)
        {
            PlayerPrefs.SetInt("EPROMIdScene", _idScene);
        }
        private int GetIDScene()
        {
            int idScene = PlayerPrefs.GetInt("EPROMIdScene");
            return idScene;
        }
        private void SetIsLoad(SettingsScene _settingsScene)
        {
            PlayerPrefs.SetInt("EPROMLoad", _settingsScene.isLoad);
        }
        private void GetIsLoad()
        {
            settingsScene.isLoad = PlayerPrefs.GetInt("EPROMLoad");
        }
        private void SetResolution(SettingsScene _settingsScene)
        {
            PlayerPrefs.SetInt("EPROMWidth", _settingsScene.Width);
            PlayerPrefs.SetInt("EPROMHeight", _settingsScene.Height);
            PlayerPrefs.SetInt("EPROMModeScreen", _settingsScene.IdCurrentModeScreen);
        }
        private void GetResolution()
        {
            settingsScene.Width = PlayerPrefs.GetInt("EPROMWidth");
            settingsScene.Height = PlayerPrefs.GetInt("EPROMHeight");
            settingsScene.IdCurrentModeScreen = PlayerPrefs.GetInt("EPROMModeScreen");
        }
        private void SetAudioParametr(SettingsScene _settingsScene)
        {
            PlayerPrefs.SetFloat("EPROMMuzVol", _settingsScene.MuzValum);
            PlayerPrefs.SetFloat("EPROMEfectVol", _settingsScene.EffectValum);
        }
        private void GetAudioParametr()
        {
            // TODO _j check all PlayerPrefs
            settingsScene.MuzValum = PlayerPrefs.GetFloat("EPROMMuzVol");
            settingsScene.EffectValum = PlayerPrefs.GetFloat("EPROMEfectVol");
        }

        #endregion

    }
}