using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class CustomButton : Button
    {
        public Action OnDown { get { return onDown; } set { onDown = value; } }
        private Action onDown;

        public Action OnUp { get { return onUp; } set { onUp = value; } }
        private Action onUp;

        private int sizeOnButton = 2;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            AnimScale(sizeOnButton);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            AnimScale(1);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            AnimScale(1);
            onDown?.Invoke();
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            AnimScale(sizeOnButton);
            onUp?.Invoke();
        }
        //
        private void AnimScale(int _scale)
        {
            gameObject.transform.localScale = new Vector3(_scale, _scale, 0);
        }

    }
}

