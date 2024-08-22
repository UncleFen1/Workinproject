using Roulettes;
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

    public Animator animator;
    
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

        mod = enemyRoulette.enemyKindsMap[EnemyKind.AttackAccuracy].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                scatterAngle /= 2;
                break;
            case EnemyModifier.Decreased:
                scatterAngle *= 2;
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

            animator.SetBool("EnemyShoot", true);

        }

        else
        {
            animator.SetBool("EnemyShoot", false);
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bullet1, shootingPoint.position, Quaternion.identity);
        var bulletInstance = bulletGO.GetComponent<EnemyBullet>();

        if (bulletInstance && bulletInstance.isActiveAndEnabled)
        {
            bulletInstance.LinkEnemyRoulette(enemyRoulette);
        }
        else
        {
            Debug.LogWarning("can't find EnemyBullet component");
        }

        Vector2 direction = (player.position - shootingPoint.position).normalized;

        float randomAngle = Random.Range(-scatterAngle, scatterAngle);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

        Vector2 scatterDirection = new Vector2(
            direction.x * Mathf.Cos(angleInRadians) - direction.y * Mathf.Sin(angleInRadians),
            direction.x * Mathf.Sin(angleInRadians) + direction.y * Mathf.Cos(angleInRadians)
        );

        float angle = Mathf.Atan2(scatterDirection.y, scatterDirection.x) * Mathf.Rad2Deg;
        bulletGO.transform.eulerAngles = new Vector3(0, 0, angle - 90f);

        Rigidbody2D rb = bulletGO.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = scatterDirection * 10f;
        }
        Destroy(bulletGO, maxFlightTime1);
    }
}