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

        private EventBus eventBus;
        [Inject]
        private void InitBindings(EventBus eb) {
            eventBus = eb;
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
                    eventBus.Raise(new RedEvent(Vector3.one * 0.15f));
                    break;

                case TypeOfEvent.Green:
                    eventBus.Raise(new GreenEvent(Vector3.one * 0.25f));
                    break;

                case TypeOfEvent.Blue:
                    eventBus.Raise(new BlueEvent(Color.cyan * Random.Range(0f, 1f)));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
