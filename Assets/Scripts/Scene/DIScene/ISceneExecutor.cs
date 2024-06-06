using System;

namespace Scene
{
    public interface ISceneExecutor
    {
        void InitScene();
        void GetSettingsScene();
        Action<SettingsScene> OnSetSettingsScene{get;set;}
        Action<int> OnOpenSceneID{get;set;}
        void OpenScenID(int scenID);

        int LoadTests();
    }
}