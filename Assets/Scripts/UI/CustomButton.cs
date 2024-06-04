using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class CustomButton : Button
    {
        [SerializeField] private Image indicatorImg;
        public Action<bool, GameObject> OnFocusMouse { get { return onFocusMouse; } set { onFocusMouse = value; } }
        private Action<bool, GameObject> onFocusMouse;
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
        public override void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("00");
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            onFocusMouse?.Invoke(false, thisObject);
        }
        private void Update()
        {

        }
    }
}

