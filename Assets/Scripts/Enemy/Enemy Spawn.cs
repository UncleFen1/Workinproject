using System.Collections.Generic;
using GameEventBus;
using GameGrid;
using OldSceneNamespace;
using Roulettes;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 10;
    public Vector2 spawnAreaSize = new Vector2(10, 10);

    private ISceneExecutor scenes;
    private EnemyRoulette enemyRoulette;
    private EnvironmentRoulette environmentRoulette;
    private List<GridController> gridControllerList;
    private EventBus eventBus;
    [Inject]
    private void InitBindings(
        EnemyRoulette er,
        EnvironmentRoulette envR,
        List<GridController> gcs,
        EventBus eb,
        ISceneExecutor sceneExecutor)
    {
        // even if disabled it is initialized
        enemyRoulette = er;
        environmentRoulette = envR;
        gridControllerList = gcs;
        eventBus = eb;
        scenes = sceneExecutor;
    }   

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 randomPosition = new Vector2(
                transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                transform.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            // 0-cultist, 1-monster
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            var go = Instantiate(randomEnemyPrefab, randomPosition, Quaternion.identity);
            go.name += i.ToString();

            // TODO _j better to create EnemyController and add all fields there
            // TODO _j maybe ZenjectBinding MonoBehaviour object to add to prefab
            var enemyHealth = go.GetComponent<EnemyHealth>();
            if (enemyHealth && enemyHealth.isActiveAndEnabled)
            {
                enemyHealth.LinkEnemyRoulette(enemyRoulette, eventBus, scenes);
            }
            var meleeEnemy = go.GetComponent<EnemyMeleeAttack>();
            if (meleeEnemy && meleeEnemy.isActiveAndEnabled)
            {
                meleeEnemy.LinkEnemyRoulette(enemyRoulette, scenes);
            }
            var rangeEnemy = go.GetComponent<EnemyShooting>();
            if (rangeEnemy && rangeEnemy.isActiveAndEnabled)
            {
                rangeEnemy.LinkEnemyRoulette(enemyRoulette, scenes);
            }
            var movementEnemy = go.GetComponent<EnemyMovement>();
            if (movementEnemy && movementEnemy.isActiveAndEnabled)
            {
                movementEnemy.LinkEnemyRoulette(enemyRoulette);
                movementEnemy.LinkEnemySpawner(this);
            }
            var enemyEnvironmentIntersection = go.GetComponent<EnemyEnvironmentIntersection>();
            if (enemyEnvironmentIntersection && enemyEnvironmentIntersection.isActiveAndEnabled)
            {
                enemyEnvironmentIntersection.LinkEnemyEnvironmentIntersection(environmentRoulette, gridControllerList);
            }
        }
    }
}