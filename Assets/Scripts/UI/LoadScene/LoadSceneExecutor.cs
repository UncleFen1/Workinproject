using System.Collections;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LoadSceneExecutor : MonoBehaviour
    {
        // [Header("Панель основная")]
        // [SerializeField] private GameObject panelGnd;
        // [SerializeField] private bool isPanelGnd = true;

        [Header("Индикатор загрузки")]
        [SerializeField] private Image loadImg;

        [Header("Текст загрузки")]
        [SerializeField] private Text loadTxt;

        // [Header("Скорость анимации перемещения панели")]
        // [SerializeField][Range(0.5f, 10f)] private float duration;
        private AsyncOperation asyncOperation;
        private bool isStopClass = false, isRun = false;
        private ISceneExecutor scenes;
        [Inject]
        public void Init(ISceneExecutor _scenes)
        {
            scenes = _scenes;
        }
        private void OnEnable()
        {

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
                OpenScene(scenes.GetOpenScenID());
            }
        }
        private void OpenScene(int _idScene)
        {
            if (isRun) { StartCoroutine(LoadSceneID(_idScene)); }
        }
        IEnumerator LoadSceneID(int _idScene)
        {
            int t = 0;
            bool isRun = true;
            yield return new WaitForSeconds(1);
            // asyncOperation=SceneManager.LoadSceneAsync(_idScene);
            // while(!asyncOperation.isDone)
            // {
            //     float progress = asyncOperation.progress / 0.9f;
            //     loadImg.fillAmount=progress;
            //     loadTxt.text=$"LOAD  {string.Format("{0:0}%", progress * 100f)}";
            //     yield return 0;
            // }

            while (isRun)
            {
                t++;
                if (t >= 101)
                {
                    isRun = false;
                    t=100;
                    
                    asyncOperation = SceneManager.LoadSceneAsync(_idScene);
                    if (!asyncOperation.isDone)
                    {
                        isRun = true;
                    }
                }
                loadImg.fillAmount = t * 0.01f;
                loadTxt.text = $"LOAD  {string.Format("{0:0}%", t)}";
                yield return 0;
           }
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
        private void DoneTween()
        {

        }
    }
}

