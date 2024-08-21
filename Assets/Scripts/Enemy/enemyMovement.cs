using Roulettes;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float turnSpeed = 10f;
    public float detectionRange = 5f;
    public float randomMovementTime = 2f;
    public float changeDirectionSpeed = 1f;

    private Vector2 randomMovementDirection = Vector2.zero;
    private float nextChangeTimeForRandomMovement;

    private Rigidbody2D _rigidbody;
    private Vector3 moveDirection;
    private Quaternion deltaRotation, directionRotation;

    EnemySpawner enemySpawner;
        
    private EnemyRoulette enemyRoulette;
    public void LinkEnemyRoulette(EnemyRoulette er)
    {
        enemyRoulette = er;
        ApplyRouletteModifiers();
    }
    void ApplyRouletteModifiers()
    {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.MovementSpeed].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                moveSpeed *= 2;
                break;
            case EnemyModifier.Decreased:
                moveSpeed /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    public void LinkEnemySpawner(EnemySpawner es)
    {
        enemySpawner = es;
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

        _rigidbody = this.GetComponent<Rigidbody2D>();
        if (_rigidbody == null) Debug.LogWarning("Can't find Rigidbody component");
    }
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                RandomMovement();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        MoveExecutor(direction);
    }

    void MoveTowardsEnemySpawner()
    {
        Vector2 direction = (enemySpawner.transform.position - transform.position).normalized;
        MoveExecutor(direction);
    }

    void RandomMovement()
    {
        if (Time.time >= nextChangeTimeForRandomMovement)
        {
            if (enemySpawner != null && !IsWithinSpawnArea())
            {
                MoveTowardsEnemySpawner();
                return;
            }
            else
            {
                randomMovementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                nextChangeTimeForRandomMovement = Time.time + randomMovementTime;
                MoveExecutor(randomMovementDirection);
            }
        }
        // MoveExecutor(randomMovementDirection);
    }

    private bool IsWithinSpawnArea()
    {
        var spawnPosition = enemySpawner.transform.position;
        var area = enemySpawner.spawnAreaSize;
        bool isWithinXArea = spawnPosition.x - area.x <= transform.position.x && transform.position.x <= spawnPosition.x + area.x;
        bool isWithinYArea = spawnPosition.y - area.y <= transform.position.y && transform.position.y <= spawnPosition.y + area.y;
        return isWithinXArea && isWithinYArea;
    }

    private void MoveExecutor(Vector2 _direction)
    {
        _rigidbody.velocity = _direction * moveSpeed;
        moveDirection = _rigidbody.velocity;

        if (moveDirection.sqrMagnitude > 0)
        {
                if (moveDirection.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (moveDirection.x > 0)
                {
                     transform.localScale = new Vector3(1f, 1f, 1f);   
                }      
        
            deltaRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        }
        directionRotation = Quaternion.RotateTowards(transform.rotation, deltaRotation, turnSpeed);
        _rigidbody.MoveRotation(directionRotation);
    }
}

