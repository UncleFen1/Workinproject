using DG.Tweening;
using UnityEngine;

namespace Anim
{
    public class AnimSplashScreen : MonoBehaviour
    {
        [Header("Подключения объекта анимации")]
        [SerializeField] private GameObject animSprite;

        [Header("Время скорости анимации")]
        [SerializeField][Range(0.5f, 10f)] private float duration;

        [Header("Путь движения анимации")]
        [SerializeField] private GameObject[] path;
        //
        private Vector3[] pathVector;
        private bool isStopClass = false, isRun = false;
        private void OnEnable()
        {

        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            pathVector = new Vector3[path.Length];
            for (int i = 0; i < path.Length; i++)
            {
                pathVector[i] = path[i].transform.position;
            }

            if (!isRun && pathVector != null)
            {
                isRun = true;
                AnimSeries();
            }
        }
        private void AnimSeries()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(animSprite.transform.DOPath(pathVector, duration, PathType.CatmullRom));
            sequence.SetLoops(-1);
            sequence.SetLink(animSprite);
            sequence.OnKill(DoneTween);
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

