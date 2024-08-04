using EnemiesUtils;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;
    public GameObject bullet1;
    public Transform shootingPoint;
    public float shootingRange = 10f;
    public float fireRate = 1f;
    public float maxFlightTime1;
    public float scatterAngle = 5f;
    private float nextFireTime = 1f;

    private EnemyRoulette enemyRoulette;
    public void LinkEnemyRoulette(EnemyRoulette er)
    {
        enemyRoulette = er;
        ApplyRouletteModifiers();
    }
    void ApplyRouletteModifiers()
    {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.AttackRange].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                shootingRange *= 2;
                break;
            case EnemyModifier.Decreased:
                shootingRange /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = enemyRoulette.enemyKindsMap[EnemyKind.AttackRate].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                fireRate *= 2;
                break;
            case EnemyModifier.Decreased:
                fireRate /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        // TODO _j Andrey setup angles and accuracy
        mod = enemyRoulette.enemyKindsMap[EnemyKind.AttackAccuracy].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                Debug.LogWarning("Andrey setup accuracy modifier");
                break;
            case EnemyModifier.Decreased:
                Debug.LogWarning("Andrey setup accuracy modifier");
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Can't find GO with Tag Player");
        }
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= shootingRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bullet1, shootingPoint.position, shootingPoint.rotation);
        var bulletInstance = bullet.GetComponent<Bullet>();
        if (bulletInstance && bulletInstance.isActiveAndEnabled)
        {
            bulletInstance.LinkEnemyRoulette(enemyRoulette);
        }

        Vector2 direction = (player.position - shootingPoint.position).normalized;

        float randomAngle = Random.Range(-scatterAngle, scatterAngle);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

        Vector2 scatterDirection = new Vector2(
            direction.x * Mathf.Cos(angleInRadians) - direction.y * Mathf.Sin(angleInRadians),
            direction.x * Mathf.Sin(angleInRadians) + direction.y * Mathf.Cos(angleInRadians)
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = scatterDirection * 10f;
        }
        Destroy(bullet, maxFlightTime1);
    }
}