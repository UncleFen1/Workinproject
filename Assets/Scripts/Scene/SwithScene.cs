using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OldSceneNamespace
{
    public class SwithScene : MonoBehaviour
    {
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void quitUnityApp();

        [Header("Указать IdScen")]
        [SerializeField] private int idLoadScen = 1;//
        [SerializeField] private int idVictoryScen = 4;
        [SerializeField] private int idOverScen = 5;
        // [SerializeField] private int idRulette3Properties = 6;
        // [SerializeField] private int idRulette4Properties = 7;
        // [SerializeField] private int idRulette8Properties = 8;
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
            scenes.OnOpenVictoryScen += OpenVictoryScen;
            scenes.OnOpenOverScen += OpenOverScen;
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
        }
        private int ReBootScenID()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            OpenScene();
            return currentScene;
        }
        private void ExitGame()
        {
            // if (isRun) { Application.Quit(); }
            Debug.Log("_j going to quit the application");
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
            Application.Quit();
#elif (UNITY_WEBGL)
            // Application.OpenURL("about:blank");
            quitUnityApp();
#endif
        }
        private int OpenVictoryScen()
        {
            OpenScene();
            return idVictoryScen;
        }
        private int OpenOverScen()
        {
            OpenScene();
            return idOverScen;
        }
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

