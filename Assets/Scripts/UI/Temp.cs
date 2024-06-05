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
        [SerializeField] private int idMainMenuScene = 0;
        [SerializeField][Range(1, 50)] private int clockButton = 1;
        [SerializeField][Range(1, 50)] private float clockScene = 10f;
        [SerializeField][Range(0.5f, 10f)] private float duration;
        private float countClockButton, timeClick, countClockSec, timeSec;
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
            if (_flag)
            {
                _objectButton.transform.DOScale(sizeOnButton, duration)
                        .SetLink(_objectButton).OnKill(DoneTween);
            }
            else
            {
                _objectButton.transform.DOScale(1, duration)
                                         .SetLink(_objectButton).OnKill(DoneTween); 
            }
        }
        private void DoneTween()
        {

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
                scenes.OpenScenID(idMainMenuScene);
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
                countClockButton++;
                if (countClockButton >= clockButton) { countClockButton = 0f; TrigerFillAmount(true); }
            }
            else
            {
                timeClick = Time.time;
                countClockButton++;
                if (countClockButton >= clockButton) { countClockButton = 0f; TrigerFillAmount(false); }
            }
            Clock();
        }
        private void Clock()
        {
            if (timeSec + 1 <= Time.time)
            {
                timeSec = Time.time;
                countClockSec++;
                if (countClockSec >= clockScene) { countClockSec = 0f; scenes.OpenScenID(idMainMenuScene); }
            }
        }
    }
}

