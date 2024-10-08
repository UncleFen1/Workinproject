using System.Collections.Generic;
using OldSceneNamespace;
using Roulettes;
using UnityEngine;
using Zenject;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;
    public int meleDamage = 40;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackAngle = 60f;
    public Transform attackSector;

    public GameObject attackEffectPrefab;
    public float effectDuration = 2f;

    [Header("Звуки эффектов")]
    [SerializeField] private AudioClip[] meleeEffectClips;
    private AudioSource effectAudioSource;

    public bool isDebugRaysShown = false;

    private ISceneExecutor scenes;
    private PlayerRoulette playerRoulette;
    [Inject]
    private void InitBindings(PlayerRoulette pr, ISceneExecutor sceneExecutor)
    {
        playerRoulette = pr;
        ApplyRouletteModifiers();

        scenes = sceneExecutor;
    }
    void ApplyRouletteModifiers()
    {
        var mod = playerRoulette.playerKindsMap[PlayerKind.MeleeDamage].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                meleDamage *= 2;
                break;
            case PlayerModifier.Decreased:
                meleDamage /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = playerRoulette.playerKindsMap[PlayerKind.AttackRate].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                attackRate *= 2;
                break;
            case PlayerModifier.Decreased:
                attackRate /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = playerRoulette.playerKindsMap[PlayerKind.AttackRange].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                attackRange *= 2;
                break;
            case PlayerModifier.Decreased:
                attackRange /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        if (attackSector == null)
        {
            Debug.LogError("Attack sector is not assigned");
        }
        if (attackPoint == null)
        {
            Debug.LogError("Attack point is not assigned");
        }

        SetupAudio();
    }

    void SetupAudio()
    {
        if (effectAudioSource == null)
        {
            effectAudioSource = gameObject.AddComponent<AudioSource>();
        }

        effectAudioSource.clip = meleeEffectClips[0];
        effectAudioSource.loop = false;

        scenes.OnSetSettingsAudioScene += (SettingsScene settingsScene) => {
            effectAudioSource.volume = settingsScene.EffectValum;
        };
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var (directionToInput, angle) = CalculateDirectionToMouse();

            Attack(directionToInput, angle);
        }
    }

    void Attack(Vector2 directionToInput, float angle)
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }

        int randomValue = Random.Range(0, meleeEffectClips.Length);
        effectAudioSource.clip = meleeEffectClips[randomValue];
        effectAudioSource.Play();

        var foundColliders = FindCollidersInSector(directionToInput);
        if (foundColliders.Count > 0)
        {
            foreach (var collider in foundColliders)
            {
                EnemyHealth enemyHealth = collider.Value.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(meleDamage);
                }
            }
        }
        SpawnAttackEffect(directionToInput, angle);

        nextAttackTime = Time.time + 1f / attackRate;
    }

    Dictionary<int, Collider2D> FindCollidersInSector(Vector2 directionToInput)
    {
        var pointA = attackPoint;
        float rangeS = attackRange;
        float aAngle = attackAngle / 2;

        // TODO _j it's an empiric increase of range, shouldn't be done this way
        rangeS *= 2f;

        // Calculate directions for lines within the sector
        Vector2[] directions = new Vector2[]
        {
            directionToInput,
            Quaternion.Euler(0, 0, aAngle) * directionToInput,
            Quaternion.Euler(0, 0, aAngle / 2f) * directionToInput,
            Quaternion.Euler(0, 0, -aAngle / 2f) * directionToInput,
            Quaternion.Euler(0, 0, -aAngle) * directionToInput
        };

        if (isDebugRaysShown)
        {
            foreach (var direction in directions)
            {
                Vector2 endPoint = (Vector2)pointA.position + direction * rangeS;
                Debug.DrawLine(pointA.position, endPoint, Color.green, 2f);
            }
        }

        Dictionary<int, Collider2D> foundColliders = new();
        foreach (var direction in directions)
        {
            var hits = Physics2D.RaycastAll(pointA.position, direction, rangeS, enemyLayers);
            AddUniqueColliders(hits, foundColliders);
        }
        return foundColliders;
    }

    private void AddUniqueColliders(RaycastHit2D[] hits, Dictionary<int, Collider2D> foundColliders)
    {
        foreach (var hit in hits)
        {
            var collider = hit.collider;
            var goInstanceId = collider.gameObject.GetInstanceID();
            if (collider.TryGetComponent<EnemyHealth>(out _))
            {
                if (!foundColliders.ContainsKey(goInstanceId))
                {
                    foundColliders.Add(goInstanceId, collider);
                }
            }
        }
    }

    private void SpawnAttackEffect(Vector2 directionToInput, float angle)
    {
        Vector2 spawnPosition = (Vector2)attackPoint.position + directionToInput * attackRange;

        if (attackEffectPrefab != null)
        {
            GameObject effect = Instantiate(attackEffectPrefab, spawnPosition, Quaternion.Euler(0, 0, angle));
            Destroy(effect, effectDuration);
        }
    }

    (Vector2 directionToInput, float angle) CalculateDirectionToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return CalculateDirectionToInput(mousePosition);
    }

    (Vector2 directionToInput, float angle) CalculateDirectionToTouch(Vector2 touchPosition)
    {
        return CalculateDirectionToInput(touchPosition);
    }

    (Vector2 directionToInput, float angle) CalculateDirectionToInput(Vector2 position)
    {
        Vector2 directionToInput = (position - (Vector2)attackPoint.position).normalized;
        float angle = Mathf.Atan2(directionToInput.y, directionToInput.x) * Mathf.Rad2Deg;

        return (directionToInput, angle);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            var (directionToInput, angle) = CalculateDirectionToMouse();
            attackSector.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            Vector3 leftBoundary = Quaternion.Euler(0, 0, -attackAngle / 2) * attackSector.right * attackRange + attackPoint.position;
            Vector3 rightBoundary = Quaternion.Euler(0, 0, attackAngle / 2) * attackSector.right * attackRange + attackPoint.position;

            Gizmos.DrawLine(attackPoint.position, leftBoundary);
            Gizmos.DrawLine(attackPoint.position, rightBoundary);
        }
    }

    public void ProcessTouchCommands(Vector2 v2)
    {
        var (directionToInput, angle) = CalculateDirectionToTouch(v2);
        Attack(directionToInput, angle);
    }
}
