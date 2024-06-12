using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scene
{
    public struct SettingsScene
    {
        public int isLoad;//true=1 false=0
        public float MuzValum;
        public float EffectValum;
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
        public Action OnReBootScen { get { return onReBootScen; } set { onReBootScen = value; } }
        private Action onReBootScen;
        public Action OnExitGame { get { return onExitGame; } set { onExitGame = value; } }
        private Action onExitGame;

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
        public void LoadScen()
        {
            // SceneData.IntTest = scenID;
            onLoadScen?.Invoke();
        }
        public void ReBootScen()
        {
            onReBootScen?.Invoke();
        }
        public void ExitGame()
        {
            onExitGame?.Invoke();
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
                settingsScene.IdCurrentModeScreen = 0;
                SetResolution(settingsScene);

                settingsScene.isLoad = 1;
                SetIsLoad(settingsScene);

            }
            Screen.SetResolution(settingsScene.Width, settingsScene.Height, (FullScreenMode)settingsScene.IdCurrentModeScreen);
        }
        private void NewSettings()
        {
            settingsScene = new SettingsScene();
            GetIsLoad();
            GetResolution();
            GetAudioParametr();
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
                        SetResolution(settingsScene);
                        Screen.SetResolution(settingsScene.Width, settingsScene.Height, (FullScreenMode)settingsScene.IdCurrentModeScreen);
                        //GetSettingsScreenScene();
                    }
                }
            }
        }
        public void NewModeScreen(int _fullScreenMode)
        {
            settingsScene.IdCurrentModeScreen = _fullScreenMode;
            SetResolution(settingsScene);
            Screen.SetResolution(settingsScene.Width, settingsScene.Height, (FullScreenMode)settingsScene.IdCurrentModeScreen);
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
            Debug.Log(_settingsScene.Width);
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
            settingsScene.MuzValum = PlayerPrefs.GetFloat("EPROMMuzVol");
            settingsScene.EffectValum = PlayerPrefs.GetFloat("EPROMEfectVol");
        }

        #endregion

    }
}