using System;

namespace Scene
{
    public class SceneExecutor : ISceneExecutor
    {

        //
        public Action<int> OnOpenSceneID { get { return onOpenSceneID; } set { onOpenSceneID = value; } }
        private Action<int> onOpenSceneID;
        //

        public void OpenScenID(int scenID)
        {
            SceneData.IntTest = scenID;
            onOpenSceneID?.Invoke(scenID);
        }
        public int LoadTests()
        {
            return SceneData.IntTest;
        }
    }
}