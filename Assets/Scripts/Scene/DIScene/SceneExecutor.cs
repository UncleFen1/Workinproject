using System;
using UnityEngine;

namespace Scene
{
    public class SceneExecutor : ISceneExecutor
    {
        private SettingsScene settingsScene;
        //
        public Action<SettingsScene> OnSetSettingsScene { get { return onSetSettingsScene; } set { onSetSettingsScene = value; } }
        private Action<SettingsScene> onSetSettingsScene;
        public Action<int> OnOpenSceneID { get { return onOpenSceneID; } set { onOpenSceneID = value; } }
        private Action<int> onOpenSceneID;
        #region SwitchScene
        public void OpenScenID(int scenID)
        {
            SceneData.IntTest = scenID;
            onOpenSceneID?.Invoke(scenID);
        }
        public int LoadTests()
        {
            return SceneData.IntTest;
        }
        #endregion

        #region SettingsScene
        public void InitScene()
        {
            if (!SceneData.SettingsScene.isLoad)
            {
                SceneData.SettingsScene.isLoad=true;
                SceneData.SettingsScene.EffectValum=0.5f;
                SceneData.SettingsScene.MuzValum=0.5f;
            }

            if (!settingsScene.isLoad)
            {
                settingsScene = new SettingsScene();
                settingsScene = SceneData.SettingsScene;
                settingsScene.isLoad = true;
            }
        }
        public void GetSettingsScene()
        {
            onSetSettingsScene?.Invoke(settingsScene);
        }
        #endregion

    }
}