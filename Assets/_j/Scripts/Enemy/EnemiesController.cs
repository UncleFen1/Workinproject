using System.Collections.Generic;
using System.Threading.Tasks;
using GameEventBus;
using OldSceneNamespace;
using UnityEngine;
using Zenject;

namespace GameEnemy
{
    public class EnemiesController : IEventReceiver<EnemyDieEvent>
    {
        // TODO _j rework all levels switch logic
        const int HARDCODED_ROULETTE_SCENE_ID = 9;

        private int monstersToKill = 0;

        private EventBus eventBus;
        private ISceneExecutor scenes;
        [Inject]
        public EnemiesController(List<EnemySpawner> ess, EventBus eb, ISceneExecutor _scenes)
        {
            Debug.Log("_j EnemiesController CONSTRUCTOR");
           
            eventBus = eb;
            scenes = _scenes;
            foreach (var enemySpawner in ess)
            {
                // TODO _j it's highly likely that is should be adjusted
                // seems like .isActiveAndEnabled isn't active on Zenject routines stage, so just .enabled
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
            Debug.Log("_j EnemiesController onDestroy");
            UnregisterEvents();
        }

        void RegisterEvents()
        {
            Debug.Log("_j EnemiesController RegisterEvents");
            eventBus.Register(this as IEventReceiver<EnemyDieEvent>);
        }

        void UnregisterEvents()
        {
            Debug.Log("_j EnemiesController UnregisterEvents");
            eventBus.Unregister(this as IEventReceiver<EnemyDieEvent>);
        }

        private void OldyOpenLvl()
        {
            // add check for _j_ExperimentScene, just to reload it
            // some strange behaviour, load scene 11, but the loaded scene is 9
            OnDestroy();

            // scenes.SetCurrentFlagRoulette(false);
            // this will always return you to lvl2, but first to roulette, logic is here LoadSceneExecutor.cs in SetClass()
            OpenSceneWithDelay();
            
            // var scene = SceneManager.GetActiveScene();
            // int currentSceneIndex = scenes.GetOpenScenID(); // better to use scene.buildIndex
            // Debug.LogWarning($"_j EnemiesController OldyOpenLvl, currentSceneIndex: {scene.buildIndex}, name: {scene.name}");
            // if (scene.name.ToLowerInvariant() == "_j_ExperimentScene".ToLowerInvariant())
            // {
            //     Debug.LogWarning($"_j EnemiesController OldyOpenLvl, special experiment");
            //     // scenes.OpenScenID(currentSceneIndex);
            //     SceneManager.LoadScene(currentSceneIndex);
            // }
            // else
            // {
            //     Debug.LogWarning($"_j EnemiesController OldyOpenLvl, ordinary");
            //     scenes.SetCurrentFlagRoulette(true);
            //     scenes.OpenScenID(currentSceneIndex);    
            // }
        }

        async void OpenSceneWithDelay()
        {
            Debug.LogWarning("_j OpenSceneWithDelay 2000");
            await Task.Delay(2000);
            scenes.OpenScenID(HARDCODED_ROULETTE_SCENE_ID);
        }

        #region IEventReceiver
        public UniqueId Id { get; } = new UniqueId();

        public void OnEvent(EnemyDieEvent @event)
        {
            monstersToKill--;
            Debug.Log($"_j EnemiesController OnEvent EnemyDieEvent monstersToKill: {monstersToKill}");

            if (monstersToKill == 0)
            {
                OldyOpenLvl();
            }
        }
        #endregion
    }
}
