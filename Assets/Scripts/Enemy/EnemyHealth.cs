using GameEventBus;
using OldSceneNamespace;
using Roulettes;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    private bool isTakingDamage = false;
    private bool isDead = false;

    public SpriteRenderer spriteRenderer;
    public Sprite deathSprite;

    [Header("Звуки эффектов")]
    [SerializeField] private AudioClip[] damageEffectClips;
    private AudioSource effectAudioSource;

    private ISceneExecutor scenes;
    private EnemyRoulette enemyRoulette;
    private EventBus eventBus;
    public void LinkEnemyRoulette(EnemyRoulette er, EventBus eb, ISceneExecutor sceneExecutor)
    {
        enemyRoulette = er;
        eventBus = eb;
        ApplyRouletteModifiers();
        
        scenes = sceneExecutor;
    }
    void ApplyRouletteModifiers()
    {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.Health].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                maxHealth *= 2;
                break;
            case EnemyModifier.Decreased:
                maxHealth /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;

        SetupAudio();
    }

    void SetupAudio()
    {
        if (effectAudioSource == null)
        {
            effectAudioSource = gameObject.AddComponent<AudioSource>();
        }

        effectAudioSource.clip = damageEffectClips[0];
        effectAudioSource.loop = false;

        scenes.OnSetSettingsAudioScene += (SettingsScene settingsScene) => {
            effectAudioSource.volume = settingsScene.EffectValum;
        };
    }

    public void TakeDamage(int damage)
    {
        int randomValue = Random.Range(0, damageEffectClips.Length);
        effectAudioSource.clip = damageEffectClips[randomValue];
        effectAudioSource.Play();

        currentHealth -= damage;
        Debug.Log($"{name} Enemy took damage: " + damage + " Current health: " + currentHealth);
        isTakingDamage = true;

        if (!isDead && currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void Update()
    {
        if (isTakingDamage)
        {
            animator.SetBool("EnemyDamage", true);
            isTakingDamage = false;
        }

        else
        {
            animator.SetBool("EnemyDamage", false);
        }
    }

    public void HealEnemy(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Enemy healed: " + value + " Current health: " + currentHealth);
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);

        spriteRenderer.sprite = deathSprite;

        if (TryGetComponent<EnemyMovement>(out EnemyMovement em)) Destroy(em);
        else Debug.LogWarning("no EnemyMovement to Destroy");
        if (TryGetComponent<EnemyEnvironmentIntersection>(out EnemyEnvironmentIntersection eei)) Destroy(eei);
        else Debug.LogWarning("no EnemyEnvironmentIntersection to Destroy");
        
        foreach (CapsuleCollider2D collider in GetComponents<CapsuleCollider2D>())
        {
            Destroy(collider);
        }
        
        if (TryGetComponent<EdgeCollider2D>(out EdgeCollider2D ec)) Destroy(ec);
        else Debug.LogWarning("no EdgeCollider2D to Destroy");
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb)) Destroy(rb);
        else Debug.LogWarning("no Rigidbody2D to Destroy");
        if (TryGetComponent<EnemyShooting>(out EnemyShooting es)) Destroy(es);
        else Debug.LogWarning("no EnemyShooting to Destroy");
        if (TryGetComponent<EnemyMeleeAttack>(out EnemyMeleeAttack ema)) Destroy(ema);
        else Debug.LogWarning("no EnemyMeleeAttack to Destroy");

        if (animator != null) Destroy(animator);

        Destroy(this);
        // Destroy(gameObject);
    }

    void Die()
    {
        animator.SetBool("EnemyDeath", true);
        Debug.Log("Enemy died!");
        eventBus.Raise(new EnemyDieEvent());
        StartCoroutine(DeathCoroutine());
    }
}