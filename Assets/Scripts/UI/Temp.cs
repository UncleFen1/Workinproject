using DG.Tweening;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class Temp : MonoBehaviour
    {
        [SerializeField] private CustomButton testButton;
        [SerializeField] private float sizeOnButton;
        [SerializeField] private Image indicatorImg;
        [SerializeField][Range(1, 100)] private float clockMSec = 50f;
        [SerializeField][Range(0.5f, 10f)] private float duration;
        private float countClockSec, timeClick;
        private float fillAmountTik = 0;
        private bool isPress;
        private ISceneExecutor scenes;
        [Inject]
        public void Init(ISceneExecutor _scenes)
        {
            scenes = _scenes;
        }
        private void OnEnable()
        {
            testButton.OnFocusMouse += ButtonSize;
            testButton.OnPressMouse += ButtonPress;
        }

        private void ButtonPress(bool _flag, GameObject _objectButton)
        {
            if (_flag)
            {
                isPress = _flag;
                ButtonSize(false, _objectButton);
            }
            else
            {
                isPress = _flag;
                ButtonSize(true, _objectButton);
            }
        }

        private void ButtonSize(bool _flag, GameObject _objectButton)
        {
            if (_flag) { _objectButton.transform.DOScale(sizeOnButton, duration); }
            else { _objectButton.transform.DOScale(1, duration); }
        }
        private void TrigerFillAmount(bool isSummCount)
        {
            if (isSummCount)
            {
                fillAmountTik += 0.1f;
            }
            else { fillAmountTik -= 0.1f; }

            if (fillAmountTik >= 1)
            {
                indicatorImg.fillAmount = 1;
            }
            else
            {
                indicatorImg.fillAmount = fillAmountTik;
            }

            if (fillAmountTik <= 0)
            {
                indicatorImg.fillAmount = 0;
            }
            else
            {
                indicatorImg.fillAmount = fillAmountTik;
            }
        }
        private void Update()
        {
            if (timeClick <= Time.time && isPress)
            {
                timeClick = Time.time;
                countClockSec++;
                if (countClockSec >= clockMSec) { countClockSec = 0f; TrigerFillAmount(true); }
            }
            else
            {
                timeClick = Time.time;
                countClockSec++;
                if (countClockSec >= clockMSec) { countClockSec = 0f; TrigerFillAmount(false); }
            }
        }
    }
}

