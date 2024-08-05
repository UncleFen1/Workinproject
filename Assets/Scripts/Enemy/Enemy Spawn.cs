using Roulettes;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 10;
    public Vector2 spawnAreaSize = new Vector2(10, 10);

    private EnemyRoulette enemyRoulette;
    [Inject]
    private void InitBindings(EnemyRoulette er) {
        enemyRoulette = er;
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
            
            var enemyHealth = go.GetComponent<EnemyHealth>();
            if (enemyHealth && enemyHealth.isActiveAndEnabled) {
                enemyHealth.LinkEnemyRoulette(enemyRoulette);
            }
            var meleeEnemy = go.GetComponent<EnemyMeleeAttack>();
            if (meleeEnemy && meleeEnemy.isActiveAndEnabled) {
                meleeEnemy.LinkEnemyRoulette(enemyRoulette);
            }
            var rangeEnemy = go.GetComponent<EnemyShooting>();
            if (rangeEnemy && rangeEnemy.isActiveAndEnabled) {
                rangeEnemy.LinkEnemyRoulette(enemyRoulette);
            }
            var movementEnemy = go.GetComponent<EnemyMovement>();
            if (movementEnemy && movementEnemy.isActiveAndEnabled) {
                movementEnemy.LinkEnemyRoulette(enemyRoulette);
            }
        }
    }

    // private void LinkRouletteWithGOComponent<T>(GameObject go) where T : Behaviour {
    //     var component = go.GetComponent<T>();
    //     if (component && component.isActiveAndEnabled) {
    //         component.LinkEnemyRoulette(enemyRoulette);
    //     }
    // }
}