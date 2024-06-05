using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class CustomButton : Button
    {
        public Action<bool, GameObject> OnFocusMouse { get { return onFocusMouse; } set { onFocusMouse = value; } }
        private Action<bool, GameObject> onFocusMouse;
        public Action<bool, GameObject> OnPressMouse { get { return onPressMouse; } set { onPressMouse = value; } }
        private Action<bool, GameObject> onPressMouse;
        private GameObject thisObject;
        protected override void Start()
        {
            thisObject = gameObject;
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            onFocusMouse?.Invoke(true, thisObject);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            onFocusMouse?.Invoke(false, thisObject);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            onPressMouse?.Invoke(true, thisObject);
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            onPressMouse?.Invoke(false, thisObject);
        }
    }
}

