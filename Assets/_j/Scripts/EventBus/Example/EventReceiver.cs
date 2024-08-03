using UnityEngine;

namespace EventBus
{
    // [RequireComponent(typeof(MeshRenderer))]
    public class EventReceiver : MonoBehaviour, IEventReceiver<RedEvent>, IEventReceiver<GreenEvent>, IEventReceiver<BlueEvent>
    {
        #region fields

        [SerializeField] private EventBusHolder _eventBusHolder;
        private SpriteRenderer _spriteRenderer;
        private Color _initialSpriteColor;

        #endregion

        #region engine methods

        // TODO _j _eventBusHolder isn't ready, DIme
        // private void OnEnable()
        private void Start()
        {
            _eventBusHolder.EventBus.Register(this as IEventReceiver<RedEvent>);
            _eventBusHolder.EventBus.Register(this as IEventReceiver<GreenEvent>);
            _eventBusHolder.EventBus.Register(this as IEventReceiver<BlueEvent>);

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _initialSpriteColor = _spriteRenderer.color;
            Debug.Log("_j _initialSpriteColor: " + _initialSpriteColor);
        }

        private void OnDestroy()
        {
            _spriteRenderer.color = _initialSpriteColor;
        }

        private void OnDisable()
        {
            _eventBusHolder.EventBus.Unregister(this as IEventReceiver<RedEvent>);
            _eventBusHolder.EventBus.Unregister(this as IEventReceiver<GreenEvent>);
            _eventBusHolder.EventBus.Unregister(this as IEventReceiver<BlueEvent>);
        }

        #endregion

        #region IEventReceiver

        public UniqueId Id { get; } = new UniqueId();

        public void OnEvent(RedEvent @event)
        {
            transform.position += @event.MoveDelta;
        }

        public void OnEvent(GreenEvent @event) => transform.localScale += @event.Scale;

        public void OnEvent(BlueEvent @event)
        {
            _spriteRenderer.color = @event.Color;
        }

        #endregion
    }
}
