using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace EventBus {
    public class EventSender : MonoBehaviour
    {
        public enum TypeOfEvent
        {
            Red,
            Green,
            Blue
        }

        private EventBus _eventBus;
        [Inject]
        private void InitBindings(EventBus eventBus) {
            _eventBus = eventBus;
        }

        #region fields

        [SerializeField] private TypeOfEvent _eventType;

        #endregion

        #region engine methods

        public void OnMouseDown()
        {
            switch (_eventType)
            {
                case TypeOfEvent.Red:
                    _eventBus.Raise(new RedEvent(Vector3.one * 0.15f));
                    break;

                case TypeOfEvent.Green:
                    _eventBus.Raise(new GreenEvent(Vector3.one * 0.25f));
                    break;

                case TypeOfEvent.Blue:
                    _eventBus.Raise(new BlueEvent(Color.cyan * Random.Range(0f, 1f)));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
