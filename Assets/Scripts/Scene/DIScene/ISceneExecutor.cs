using System;

namespace Scene
{
    public interface ISceneExecutor
    {
        Action<int> OnOpenSceneID{get;set;}
        void OpenScenID(int scenID);

        int LoadTests();
    }
}