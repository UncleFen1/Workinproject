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
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            int randomValue = Random.Range(0, meleeEffectClips.Length);
            effectAudioSource.clip = meleeEffectClips[randomValue];
            effectAudioSource.Play();

            var (directionToMouse, angle) = CalculateDirectionToMouseAndAngle();

            Attack(directionToMouse, angle);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack(Vector2 directionToMouse, float angle)
    {
        var foundColliders = FindCollidersInSector(directionToMouse);
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
        SpawnAttackEffect(directionToMouse, angle);
    }

    Dictionary<int, Collider2D> FindCollidersInSector(Vector2 directionToMouse)
    {
        var pointA = attackPoint;
        float rangeS = attackRange;
        float aAngle = attackAngle / 2;

        // TODO _j it's an empiric increase of range, shouldn't be done this way
        rangeS *= 2f;

        Vector2 pointB = (Vector2)pointA.position + directionToMouse * rangeS;
        
        // Calculate point C (Counterclockwise by aAngle)
        Vector2 directionAC = Quaternion.Euler(0, 0, aAngle) * directionToMouse;
        Vector2 pointC = (Vector2)pointA.position + directionAC * rangeS;

        // Calculate point E (Counterclockwise by aAngle/2)
        Vector2 directionAE = Quaternion.Euler(0, 0, aAngle/2f) * directionToMouse;
        Vector2 pointE = (Vector2)pointA.position + directionAE * rangeS;

        // Calculate point D (Clockwise by aAngle)
        Vector2 directionAD = Quaternion.Euler(0, 0, -aAngle) * directionToMouse;
        Vector2 pointD = (Vector2)pointA.position + directionAD * rangeS;
        
        // Calculate point F (Clockwise by aAngle/2)
        Vector2 directionAF = Quaternion.Euler(0, 0, -aAngle/2f) * directionToMouse;
        Vector2 pointF = (Vector2)pointA.position + directionAF * rangeS;
        
        // Draw the lines for visualization in the scene view
        Debug.DrawLine(pointA.position, pointB, Color.green, 2f);
        Debug.DrawLine(pointA.position, pointC, Color.green, 2f);
        Debug.DrawLine(pointA.position, pointD, Color.green, 2f);
        Debug.DrawLine(pointA.position, pointE, Color.green, 2f);
        Debug.DrawLine(pointA.position, pointF, Color.green, 2f);

        // Perform raycasts along AB, AC, AD
        var hitAB = Physics2D.RaycastAll(pointA.position, directionToMouse, rangeS, enemyLayers);
        var hitAC = Physics2D.RaycastAll(pointA.position, directionAC, rangeS, enemyLayers);
        var hitAD = Physics2D.RaycastAll(pointA.position, directionAD, rangeS, enemyLayers);
        var hitAE = Physics2D.RaycastAll(pointA.position, directionAE, rangeS, enemyLayers);
        var hitAF = Physics2D.RaycastAll(pointA.position, directionAF, rangeS, enemyLayers);

        Dictionary<int, Collider2D> foundColliders = new();
        
        for (int i = 0; i < hitAB.Length; i++)
        {
            var collider = hitAB[i].collider;
            var goInstanceId = collider.gameObject.GetInstanceID();
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth eh))
            {
                if (!foundColliders.ContainsKey(goInstanceId))
                {
                    foundColliders.Add(goInstanceId, collider);
                }
            }
        }
        for (int i = 0; i < hitAC.Length; i++)
        {
            var collider = hitAC[i].collider;
            var goInstanceId = collider.gameObject.GetInstanceID();
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth eh))
            {
                if (!foundColliders.ContainsKey(goInstanceId))
                {
                    foundColliders.Add(goInstanceId, collider);
                }
            }
        }
        for (int i = 0; i < hitAD.Length; i++)
        {
            var collider = hitAD[i].collider;
            var goInstanceId = collider.gameObject.GetInstanceID();
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth eh))
            {
                if (!foundColliders.ContainsKey(goInstanceId))
                {
                    foundColliders.Add(goInstanceId, collider);
                }
            }
        }
        for (int i = 0; i < hitAE.Length; i++)
        {
            var collider = hitAE[i].collider;
            var goInstanceId = collider.gameObject.GetInstanceID();
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth eh))
            {
                if (!foundColliders.ContainsKey(goInstanceId))
                {
                    foundColliders.Add(goInstanceId, collider);
                }
            }
        }
        for (int i = 0; i < hitAF.Length; i++)
        {
            var collider = hitAF[i].collider;
            var goInstanceId = collider.gameObject.GetInstanceID();
            if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth eh))
            {
                if (!foundColliders.ContainsKey(goInstanceId))
                {
                    foundColliders.Add(goInstanceId, collider);
                }
            }
        }
        // string foundColliderNames = "";
        // foreach (var kv in foundColliders) foundColliderNames += $"{kv.Value.name}, ";
        // Debug.Log($"_j pm, foundColliderNames: {foundColliderNames}");
        return foundColliders;
    }

    private void SpawnAttackEffect(Vector2 directionToMouse, float angle)
    {
        Vector2 spawnPosition = (Vector2)attackPoint.position + directionToMouse * attackRange;

        if (attackEffectPrefab != null)
        {
            GameObject effect = Instantiate(attackEffectPrefab, spawnPosition, Quaternion.Euler(0, 0, angle));
            Destroy(effect, effectDuration);
        }
    }

    (Vector2 directionToMouse, float angle) CalculateDirectionToMouseAndAngle()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mousePosition - (Vector2)attackPoint.position).normalized;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        return (directionToMouse, angle);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            var (directionToMouse, angle) = CalculateDirectionToMouseAndAngle();
            attackSector.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);

            Vector3 leftBoundary = Quaternion.Euler(0, 0, -attackAngle / 2) * attackSector.right * attackRange + attackPoint.position;
            Vector3 rightBoundary = Quaternion.Euler(0, 0, attackAngle / 2) * attackSector.right * attackRange + attackPoint.position;

            Gizmos.DrawLine(attackPoint.position, leftBoundary);
            Gizmos.DrawLine(attackPoint.position, rightBoundary);
        }
    }
}
