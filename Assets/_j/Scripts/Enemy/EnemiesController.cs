using System.Collections.Generic;
using GameEventBus;
using UnityEngine;
using Zenject;

namespace GameEnemy
{
    public class EnemiesController : IEventReceiver<EnemyDieEvent>
    {
        private EventBus eventBus;
        [Inject]
        public EnemiesController(List<EnemySpawner> ess, EventBus eb)
        {
            Debug.LogWarning("_j EnemiesController CONSTRUCTOR");

            eventBus = eb;

            Debug.LogWarning($"_j ess.count: {ess.Count}");
            foreach (var enemySpawner in ess)
            {
                Debug.LogWarning($"_j ess {enemySpawner.gameObject.GetInstanceID()} expect: {enemySpawner.enemyCount}");
            }

            RegisterEvents();
        }

        private void OnDestroy()
        {
            // TODO _j have to be sure that it runs and check switch between levels
            // may be make it MonoBehaviour to automatically Destroy on SceneChange
            Debug.LogWarning("_j EnemyDieEvent onDestroy");
            UnregisterEvents();
        }

        void RegisterEvents()
        {
            eventBus.Register(this as IEventReceiver<EnemyDieEvent>);
        }

        void UnregisterEvents()
        {
            eventBus.Unregister(this as IEventReceiver<EnemyDieEvent>);
        }

        #region IEventReceiver
        public UniqueId Id { get; } = new UniqueId();

        public void OnEvent(EnemyDieEvent @event)
        {
            Debug.LogWarning("_j EnemyDieEvent not implemented");
        }
        #endregion
    }
}
