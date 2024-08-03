using UnityEngine;
using Zenject;

namespace EventBus
{
    // [RequireComponent(typeof(MeshRenderer))]
    public class EventReceiver : MonoBehaviour, IEventReceiver<RedEvent>, IEventReceiver<GreenEvent>, IEventReceiver<BlueEvent>
    {
        private EventBus _eventBus;
        [Inject]
        private void InitBindings(EventBus eventBus) {
            _eventBus = eventBus;
        }

        #region fields

        private SpriteRenderer _spriteRenderer;
        private Color _initialSpriteColor;

        #endregion

        #region engine methods

        // private void OnEnable()
        private void Start()
        {
            _eventBus.Register(this as IEventReceiver<RedEvent>);
            _eventBus.Register(this as IEventReceiver<GreenEvent>);
            _eventBus.Register(this as IEventReceiver<BlueEvent>);

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
            _eventBus.Unregister(this as IEventReceiver<RedEvent>);
            _eventBus.Unregister(this as IEventReceiver<GreenEvent>);
            _eventBus.Unregister(this as IEventReceiver<BlueEvent>);
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
