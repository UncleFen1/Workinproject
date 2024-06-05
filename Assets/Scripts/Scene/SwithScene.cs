using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scene
{
    public class SwithScene : MonoBehaviour
    {
        private ISceneExecutor scenes;
        [Inject]
        public void Init(ISceneExecutor _scenes)
        {
            scenes = _scenes;
        }
        private void OnEnable()
        {
            scenes.OnOpenSceneID += OpenScene;
        }
        private void OpenScene(int _idScene)
        {
            Debug.Log(scenes.LoadTests());
            SceneManager.LoadScene(_idScene);
        }
    }
}

