using Roulettes;
using System.Collections;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float detectionRange = 5f;
    public float randomMovementTime = 2f;
    public float changeDirectionSpeed = 1f;
    public float idleTime = 5f;

    private bool isWalking = false;
    public Animator animator;

    private Vector2 movementDirection;
    private float nextChangeTime;

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
                speed *= 2;
                break;
            case EnemyModifier.Decreased:
                speed /= 2;
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

        StartCoroutine(Idle());
    }
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                StopAllCoroutines();
                MoveTowardsPlayer();
                isWalking = true;
            }

            animator.SetBool("EnemyWalking", isWalking);
        }
    }

        void MoveTowardsPlayer()
        {
            Vector2 direction = (player.position - transform.position).normalized;
            isWalking = true;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
        }

        IEnumerator RandomMovement()
        {

            while (true)
            {
                if (Time.time >= nextChangeTime)
                {

                    movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                    nextChangeTime = Time.time + randomMovementTime;
                }

                isWalking = true;

                if (movementDirection.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (movementDirection.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                transform.position += (Vector3)movementDirection * speed * Time.deltaTime;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange)

            {
                yield break;
            }

            yield return null;
            }
        }

        IEnumerator Idle()
        {
                isWalking = false;
                animator.SetBool("EnemyWalking", false);

                while (true)
                {
                    yield return new WaitForSeconds(idleTime);

                    StartCoroutine(RandomMovement());

                    yield return new WaitForSeconds(idleTime);

                }
        }
     
    }
