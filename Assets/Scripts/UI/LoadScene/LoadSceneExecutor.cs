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

        [Header("Скорость анимации перемещения панели")]
        [SerializeField][Range(0.5f, 10f)] private float duration;

        private AsyncOperation asyncOperation;
        //private GameObject tempAnimObject;
        private bool isStopClass = false, isRun = false;
        private ISceneExecutor scenes;
        [Inject]
        public void Init(ISceneExecutor _scenes)
        {
            scenes = _scenes;
        }
        private void OnEnable()
        {
            //scenes.OnOpenSceneID += OpenScene;
            // tempAnimObject = panelGnd;
            // if (isPanelGnd) { panel.OnGndPanel += GndPanel; }
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                // StartCoroutine(LoadSceneID(5));
                isRun = true;
                //GndPanel();
                OpenScene(scenes.GetOpenScenID());
            }
        }
        private void OpenScene(int _idScene)
        {
            if (isRun) { StartCoroutine(LoadSceneID(_idScene)); }
        }
        IEnumerator LoadSceneID(int _idScene)
        {
            yield return new WaitForSeconds(1);
            asyncOperation=SceneManager.LoadSceneAsync(_idScene);
            while(!asyncOperation.isDone)
            {
                float progress = asyncOperation.progress / 0.9f;
                loadImg.fillAmount=progress;
                loadTxt.text=$"LOAD  {string.Format("{0:0}%", progress * 100f)}";
                yield return 0;
            }
        }
        // private void GndPanel()
        // {
        //     MovePanel(panelGnd);
        // }
        // private void MovePanel(GameObject _objectButton)
        // {
        //     Sequence sequence = DOTween.Sequence();
        //     sequence.Append(tempAnimObject.transform.DOMove(startPointPanel.transform.position, duration));
        //     tempAnimObject = _objectButton;
        //     sequence.Append(_objectButton.transform.DOMove(new Vector3(0, 0, 0), duration));
        //     sequence.SetLink(_objectButton);
        //     sequence.OnKill(DoneTween);
        // }
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

