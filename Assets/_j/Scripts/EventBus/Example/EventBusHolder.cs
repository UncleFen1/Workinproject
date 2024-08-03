﻿using UnityEngine;

namespace EventBus {
    public class EventBusHolder : MonoBehaviour
    {
        public EventBus EventBus { get; private set; }

        private void Awake() => EventBus = new EventBus();
    }
}
