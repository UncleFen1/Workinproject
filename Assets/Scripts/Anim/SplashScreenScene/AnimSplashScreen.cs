using UnityEngine;
using UnityEngine.UI;

namespace Anim
{
    public class AnimSplashScreen : MonoBehaviour
    {
        [Header("Подключения объекта анимации")]
        [SerializeField] private GameObject animSprite;

        [Header("Время скорости анимации")]
        [SerializeField][Range(1f, 60f)] private float duration = 1;
        //
        private SpriteRenderer spriteRenderer;
        private Color countColor;
        private bool isTrig = true;
        private float clock, countClock = 0f;
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
            if (!isRun)
            {
                spriteRenderer = animSprite.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    isRun = true;
                    countColor = spriteRenderer.color;
                }
            }
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
            Clock();
        }
        private void Anim()
        {
            if (isTrig)
            {
                countColor.a = countColor.a + 0.1f;
                if (countColor.a >= 1) { isTrig = false; }
            }
            else
            {
                countColor.a = countColor.a - 0.1f;
                if (countColor.a <= 0) { isTrig = true; }
            }

            spriteRenderer.color = countColor;
        }
        private void Clock()
        {
            countClock++;
            if (countClock >= duration)
            {
                Anim();
                countClock = 0f;
            }

            // if (clock + 1 <= Time.time)
            // {
            //     clock = Time.time;
            //     countClock++;
            //     if (countClock >= duration) { Anim(); countClock = 0f; }
            // }
        }
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

