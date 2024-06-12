using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scene
{
    public class SwithScene : MonoBehaviour
    {
        [SerializeField] private int idLoadScen = 2;
        private bool isStopClass = false, isRun = false;
        //
        private ISceneExecutor scenes;
        [Inject]
        public void Init(ISceneExecutor _scenes)
        {
            scenes = _scenes;
        }
        private void OnEnable()
        {
            scenes.OnOpenSceneID += OpenScene;
            scenes.OnReBootScen += ReBootScenID;
            scenes.OnExitGame += ExitGame;
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                isRun = true;
            }
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void OpenScene()
        {
            if (isRun) { SceneManager.LoadScene(idLoadScen); }
            // if (isRun) { StartCoroutine(LoadYourAsyncScene(_idScene)); }
        }
        // IEnumerator LoadYourAsyncScene(int _idScene)
        // {
        //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_idScene);
        //     while (!asyncLoad.isDone)
        //     {
        //         yield return null;
        //     }
        // }
        private void ReBootScenID()
        {
            if (isRun) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        }
        private void ExitGame()
        {
            if (isRun) { Application.Quit(); }
        }
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

