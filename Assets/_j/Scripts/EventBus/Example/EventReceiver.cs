using UnityEngine;
using Zenject;

namespace GameEventBus
{
    // [RequireComponent(typeof(MeshRenderer))]
    public class EventReceiver : MonoBehaviour, IEventReceiver<RedEvent>, IEventReceiver<GreenEvent>, IEventReceiver<BlueEvent>
    {
        private EventBus eventBus;
        [Inject]
        private void InitBindings(EventBus eb) {
            eventBus = eb;
        }

        #region fields

        private SpriteRenderer _spriteRenderer;
        private Color _initialSpriteColor;

        #endregion

        #region engine methods

        // private void OnEnable()
        private void Start()
        {
            eventBus.Register(this as IEventReceiver<RedEvent>);
            eventBus.Register(this as IEventReceiver<GreenEvent>);
            eventBus.Register(this as IEventReceiver<BlueEvent>);

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _initialSpriteColor = _spriteRenderer.color;
        }

        private void OnDestroy()
        {
            _spriteRenderer.color = _initialSpriteColor;
        }

        private void OnDisable()
        {
            eventBus.Unregister(this as IEventReceiver<RedEvent>);
            eventBus.Unregister(this as IEventReceiver<GreenEvent>);
            eventBus.Unregister(this as IEventReceiver<BlueEvent>);
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
