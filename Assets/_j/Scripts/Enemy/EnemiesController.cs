using System.Collections.Generic;
using GameEventBus;
using OldSceneNamespace;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace GameEnemy
{
    public class EnemiesController : IEventReceiver<EnemyDieEvent>
    {
        private int monstersToKill = 0;

        private EventBus eventBus;
        private ISceneExecutor scenes;
        [Inject]
        public EnemiesController(List<EnemySpawner> ess, EventBus eb, ISceneExecutor _scenes)
        {
            eventBus = eb;
            scenes = _scenes;
            foreach (var enemySpawner in ess)
            {
                // TODO _j it's highly likely that is should be adjusted
                // seems like isActiveAndEnabled isn't active on Zenject
                //if (enemySpawner.isActiveAndEnabled)
                if (enemySpawner.enabled)
                {
                    monstersToKill += enemySpawner.enemyCount;
                }
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

        private void OpenLvl()
        {
            // add check for _j_ExperimentScene

            scenes.SetCurrentFlagRoulette(true);
            scenes.OpenScenID(scenes.GetOpenScenID());
            // TODO _j check this https://stackoverflow.com/a/25765030
            //this.Dispose();
        }

        #region IEventReceiver
        public UniqueId Id { get; } = new UniqueId();

        public void OnEvent(EnemyDieEvent @event)
        {
            monstersToKill--;
            Debug.Log($"_j EnemiesController OnEvent monstersToKill: {monstersToKill}");

            if (monstersToKill == 0)
            {
                OpenLvl();
            }
        }
        #endregion
    }
}
