using UnityEngine;

namespace GameEventBus
{
    // TODO _j don't like struct
    public readonly struct RedEvent : IEvent
    {
        public readonly Vector3 MoveDelta;

        public RedEvent(Vector3 moveDelta)
        {
            MoveDelta = moveDelta;
        }
    }

    public readonly struct GreenEvent : IEvent
    {
        public readonly Vector3 Scale;

        public GreenEvent(Vector3 scale)
        {
            Scale = scale;
        }
    }

    public readonly struct BlueEvent : IEvent
    {
        public readonly Color Color;

        public BlueEvent(Color color)
        {
            Color = color;
        }
    }

    public class EnemyDieEvent : IEvent
    {
        public int anyVariableToAdd;
    }
}
