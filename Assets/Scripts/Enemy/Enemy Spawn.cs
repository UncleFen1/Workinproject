using System.Collections.Generic;
using GameEventBus;
using GameGrid;
using Roulettes;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 10;
    public Vector2 spawnAreaSize = new Vector2(10, 10);

    private EnemyRoulette enemyRoulette;
    private EnvironmentRoulette environmentRoulette;
    private List<GridController> gridControllerList;
    private EventBus eventBus;
    [Inject]
    private void InitBindings(EnemyRoulette er, EnvironmentRoulette envR, List<GridController> gcs, EventBus eb)
    {
        // even if disabled it is initialized
        enemyRoulette = er;
        environmentRoulette = envR;
        gridControllerList = gcs;
        eventBus = eb;
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
                enemyHealth.LinkEnemyRoulette(enemyRoulette, eventBus);
            }
            var meleeEnemy = go.GetComponent<EnemyMeleeAttack>();
            if (meleeEnemy && meleeEnemy.isActiveAndEnabled)
            {
                meleeEnemy.LinkEnemyRoulette(enemyRoulette);
            }
            var rangeEnemy = go.GetComponent<EnemyShooting>();
            if (rangeEnemy && rangeEnemy.isActiveAndEnabled)
            {
                rangeEnemy.LinkEnemyRoulette(enemyRoulette);
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