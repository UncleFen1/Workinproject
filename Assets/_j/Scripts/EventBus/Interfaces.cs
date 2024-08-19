namespace GameEventBus {
    public interface IEvent{}

    public interface IBaseEventReceiver
    {
        public UniqueId Id { get; }
    }

    public interface IEventReceiver<T> : IBaseEventReceiver where T : IEvent
    {
        void OnEvent(T @event);
    }
}
