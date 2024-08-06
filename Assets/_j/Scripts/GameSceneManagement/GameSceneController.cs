using Roulettes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameSceneManagement
{
    public class GameSceneController
    {
        private int previousSceneIndex = int.MinValue;

        public GameSceneController()
        {
            SceneManager.activeSceneChanged += ChangedActiveScene;

            // previousScene = SceneManager.GetActiveScene();
        }

        EnvironmentRoulette environmentRoulette;
        EnemyRoulette enemyRoulette;
        PlayerRoulette playerRoulette;

        [Inject]
        private void InitBindings(EnvironmentRoulette envR, EnemyRoulette er, PlayerRoulette pr)
        {
            environmentRoulette = envR;
            enemyRoulette = er;
            playerRoulette = pr;
        }

        // TODO _j for some reason fires several times on same scene (Menu, Roulette, lvl)
        // TODO _j in Hierarchy you could see how several scenes are created O_O
        private void ChangedActiveScene(Scene current, Scene nextScene)
        {
            // current scene is always -1 for some reason, probably because of dynamic load, so this euristic was written
            if (previousSceneIndex != nextScene.buildIndex)
            {
                string prevSceneName = string.Empty;
                if (previousSceneIndex != int.MinValue)
                {
                    prevSceneName = SceneManager.GetSceneByBuildIndex(previousSceneIndex).name;
                }
                Debug.Log($"Scene is changed from: {previousSceneIndex} '{prevSceneName}', next scene name: {nextScene.buildIndex} '{nextScene.name}'");

                string sceneNameInvariant = nextScene.name.ToLowerInvariant();
                if (sceneNameInvariant.Contains("roulette"))
                {
                    environmentRoulette.NextRoll();
                    enemyRoulette.NextRoll();
                    playerRoulette.NextRoll();
                }

                previousSceneIndex = nextScene.buildIndex;
            }
        }
    }
}
