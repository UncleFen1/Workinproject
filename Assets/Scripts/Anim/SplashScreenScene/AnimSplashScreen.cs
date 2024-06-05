using DG.Tweening;
using UnityEngine;

namespace Anim
{
    public class AnimSplashScreen : MonoBehaviour
    {
        [SerializeField] private Transform animSprite;
        [SerializeField][Range(0.5f, 10f)] private float duration;
       // [SerializeField] private GameObject posA, posW, posD, posS;
        [SerializeField] private GameObject[] path;
        private Sequence sequence;
        private Vector3[] pathVector;
        void Start()
        {
            pathVector=new Vector3[path.Length];
            for (int i = 0; i < path.Length; i++)
            {
                pathVector[i] = path[i].transform.position;
            }
            sequence = DOTween.Sequence();
            
            Series();
            //

        }
        private void Series()
        {
            // sequence.Append(animSprite.DOMove(posA.transform.position, duration)).PlayForward();
            // sequence.Join(animSprite.DORotate(posA.transform.rotation.eulerAngles, duration, RotateMode.Fast));
            // //sequence.Join(animSprite.DORotate(new Vector3(0, 180, 0), duration, RotateMode.Fast));
            // //sequence.AppendInterval(1);
            // sequence.Append(animSprite.DOMove(posW.transform.position, duration)).PlayForward();
            // sequence.Join(animSprite.DORotate(new Vector3(0, 180, 0), duration, RotateMode.Fast));
            // //sequence.AppendInterval(1);
            // sequence.Append(animSprite.DOMove(posD.transform.position, duration)).PlayForward();
            // sequence.Join(animSprite.DORotate(new Vector3(0, 180, 0), duration, RotateMode.Fast));
            // //sequence.AppendInterval(1);
            // sequence.Append(animSprite.DOMove(posS.transform.position, duration)).PlayForward();
            // sequence.Join(animSprite.DORotate(new Vector3(0, 180, 0), duration, RotateMode.Fast));
            // //sequence.AppendInterval(1);
            // sequence.Append(animSprite.DOMove(posA.transform.position, duration)).PlayForward();
            // sequence.Join(animSprite.DORotate(posA.transform.rotation.eulerAngles, duration, RotateMode.Fast));
            // sequence.SetLoops(-1);

            //sequence.OnComplete(CompleteTween);

            // sequence.SetSpeedBased(true);
            //  sequence.Append(animSprite.DOPath(pathVector, duration,PathType.CatmullRom));
            //  sequence.Join(animSprite.DORotate(new Vector3(0, 0, 180), duration, RotateMode.Fast));
            //  sequence.SetEase(Ease.Linear);
             
            // //sequence.Append(animSprite.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)).SetEase(Ease.Linear);
            // sequence.SetLoops(-1, LoopType.Restart);
            animSprite.DOPath(pathVector, duration,PathType.CatmullRom).SetLoops(-1);

            
        }
    }
}

